using Microsoft.AspNetCore.Mvc;
using GymStudioOS.Models.Gym.Data;
using GymStudioOS.Models.Gym.View;
using GymStudioOS.Repositories;
using GymStudioOS.Constants;

namespace GymStudioOS.Controllers
{
    public class MembersController : Controller
    {
        private readonly IRepository<GymUserRole> _userRoleRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<GymBranch> _branchRepository;

        public MembersController(IRepository<GymUserRole> userRoleRepository, IRepository<UserProfile> userProfileRepository, IRepository<GymBranch> branchRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userProfileRepository = userProfileRepository;
            _branchRepository = branchRepository;
        }

        public async Task<IActionResult> ManageMembers(int gymId)
        {
            ViewBag.GymId = gymId;

            IEnumerable<GymUserRole> members = await _userRoleRepository
                .Where(r => r.GymId == gymId && r.Role == GymRoles.Member);

            // Avoid concurrent DbContext operations by using sequential async calls
            var memberProfiles = new List<UserProfileListItemVm>();
            foreach (var memberRole in members)
            {
                var profile = await SelectProfile(memberRole);
                memberProfiles.Add(profile);
            }

            var model = new ManageMembersVm
            {
                TotalMembers = memberProfiles.Count(),
                ActiveMembers = memberProfiles.Count(),
                NewThisMonth = memberProfiles.Count(m => m != null),
                NewMembersGrowthPercent = 12,
                RetentionRate = 94.2,
                Members = memberProfiles
            };

            return View(model);
        }

        private async Task<UserProfileListItemVm> SelectProfile(GymUserRole role)
        {
            UserProfile? member = await _userProfileRepository.GetFirstOrDefaultAsync(m => m.UserId == role.UserId);
            if (member == null)
            {
                throw new Exception("User profile not found for the given member role.");
            }
            GymBranch? branch = null;   
            if (role.BranchId != null)
            {
                branch = await _branchRepository.GetByIdAsync(role.BranchId ?? 0);
            }
            var random = new Random();
            var daysAgo = random.Next(1, 60); // Random number of days between 1 and 59
            var lastVisit = DateTime.Now.AddDays(-daysAgo);

            return new UserProfileListItemVm
            {
                UserId = member.UserId,
                FullName = $"{member.FirstName} {member.LastName}",
                JoinedAt = role.AssignedAt,
                Email = member.Email,
                PhoneNumber = member.Phone,
                MembershipType = "Standard",
                MembershipStatus = "Active",
                BranchName = branch?.Name ?? "All Branches",
                LastVisit = lastVisit
            };
        }
    }
}
