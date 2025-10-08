using GymStudioOS.Models.Gym.Data;

namespace GymStudioOS.Models.Gym.View
{
    public class ManageMembersVm
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int NewThisMonth { get; set; }
        public int NewMembersGrowthPercent { get; set; }
        public double RetentionRate { get; set; }
        public IEnumerable<UserProfileListItemVm> Members { get; set; } = new List<UserProfileListItemVm>();
    }

    public class UserProfileListItemVm
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MembershipType { get; set; } = string.Empty;
        public string MembershipStatus { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public DateTime LastVisit { get; set; }
    }
}
