using GymStudioOS.Constants;
using GymStudioOS.Data;
using GymStudioOS.Models.Gym.Data;
using GymStudioOS.Models.Gym.View;
using GymStudioOS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymStudioOS.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<GymUserRole> _gymUserRoleRepository;
        private readonly IRepository<GymBranch> _gymBranchRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(
            IRepository<UserProfile> userProfileRepository,
            IRepository<GymUserRole> gymUserRoleRepository,
            IRepository<GymBranch> gymBranchRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<UserProfileController> logger)
        {
            _userProfileRepository = userProfileRepository;
            _gymUserRoleRepository = gymUserRoleRepository;
            _gymBranchRepository = gymBranchRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            _logger.LogInformation($"Edit GET: userId={userId}");
            if (userId == null)
            {
                _logger.LogWarning("Edit GET: userId is null, redirecting to login.");
                return RedirectToAction("Login", "Account");
            }
            var profile = await _userProfileRepository.GetFirstOrDefaultAsync(up => up.UserId == userId);
            if (profile == null)
            {
                _logger.LogWarning($"Edit GET: No profile found for userId={userId}");
                return NotFound("User profile not found.");
            }
            var email = profile.Email ?? user?.Email;
            var vm = new UserProfileVM
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = email,
                Phone = profile.Phone,
                PersonalId = profile.PersonalId,
                Bio = profile.Bio,
                UserId = profile.UserId
            };
            _logger.LogInformation($"Edit GET: Loaded profile for userId={userId}, FirstName={vm.FirstName}, LastName={vm.LastName}, Email={vm.Email}, Phone={vm.Phone}, Bio={vm.Bio}");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileVM model)
        {
            _logger.LogInformation($"Edit POST: ModelState.IsValid={ModelState.IsValid}, UserId={model.UserId}, FirstName={model.FirstName}, LastName={model.LastName}, Email={model.Email}, Phone={model.Phone}, Bio={model.Bio}");
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"ModelState error: {error.ErrorMessage}");
                }
                _logger.LogWarning("Edit POST: ModelState is invalid.");
                return View(model);
            }
            // Fetch the existing profile by Id
            var existingProfile = await _userProfileRepository.GetByIdAsync(model.Id);
            if (existingProfile == null)
            {
                _logger.LogWarning($"Edit POST: No existing profile found for Id={model.Id}, creating new profile.");
                var newProfile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Bio = model.Bio,
                    UserId = model.UserId
                };
                await _userProfileRepository.AddAsync(newProfile);
            }
            else
            {
                // Update properties
                existingProfile.FirstName = model.FirstName;
                existingProfile.LastName = model.LastName;
                existingProfile.Email = model.Email;
                existingProfile.Phone = model.Phone;
                existingProfile.Bio = model.Bio;
                await _userProfileRepository.UpdateAsync(existingProfile);
                _logger.LogInformation("Edit POST: Profile updated successfully.");
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> RegisterMember(int gymId)
        {
            var vm = new RegisterMemberVm { GymId = gymId };
            var branches = (await _gymBranchRepository.GetAllAsync()).Where(b => b.GymId == gymId)
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();
            ViewBag.Branches = branches;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMember(RegisterMemberVm model, string action)
        {
            var branches = (await _gymBranchRepository.GetAllAsync()).Where(b => b.GymId == model.GymId)
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();
            ViewBag.Branches = branches;
            _logger.LogInformation($"RegisterMember POST: GymId={model.GymId}");
            // Handle search
            if (action == "search")
            {
                model.SearchPerformed = true;
                // Search by unique fields: email or personal ID
                var foundProfile = await _userProfileRepository.GetFirstOrDefaultAsync(
                    up => up.Email == model.SearchTerm || up.PersonalId == model.SearchTerm
                );
                if (foundProfile != null)
                {
                    model.FoundUser = new UserProfileVM
                    {
                        Id = foundProfile.Id,
                        FirstName = foundProfile.FirstName,
                        LastName = foundProfile.LastName,
                        Email = foundProfile.Email,
                        UserId = foundProfile.UserId
                    };
                    model.ShowRegisterForm = false;
                }
                else
                {
                    model.FoundUser = null;
                    model.ShowRegisterForm = true;
                }
                return View(model);
            }
            // Show registration form directly
            if (action == "showRegister")
            {
                model.SearchPerformed = false;
                model.ShowRegisterForm = true;
                model.FoundUser = null;
                return View(model);
            }
            // Register new user and assign role
            if (action == "register")
            {
                var user = new ApplicationUser { UserName = model.NewUser.Email, Email = model.NewUser.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, model.NewUser.PersonalId);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRoles.User);
                    var userProfile = new UserProfile
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = model.NewUser.FirstName,
                        LastName = model.NewUser.LastName,
                        Phone = model.NewUser.Phone,
                        PersonalId = model.NewUser.PersonalId,
                        Bio = model.NewUser.Bio
                    };
                    await _userProfileRepository.AddAsync(userProfile);
                    var gymUserRole = new GymUserRole
                    {
                        UserId = user.Id,
                        Role = model.SelectedRole,
                        GymId = model.GymId,
                        BranchId = model.SelectedRole == GymRoles.Member ? null : model.BranchId
                    };
                    await _gymUserRoleRepository.AddAsync(gymUserRole);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        _logger.LogWarning($"Register POST: Error - {error.Description}");
                    }
                    return View(model);
                }
                _logger.LogInformation($"Register POST: Registered new user with Id={user.Id}, assigned role '{model.SelectedRole}' for gymId={model.GymId}");
                return RedirectToAction("Dashboard", "Gym", new { gymId = model.GymId });
            }
            // Assign role to found user
            if (action == "assign")
            {
                if (model.FoundUser == null)
                {
                    ModelState.AddModelError(string.Empty, "No user selected to assign role.");
                    _logger.LogWarning("Assign POST: No user selected to assign role.");
                    return View(model);
                }
                if (model.SelectedRole != null && model.FoundUser != null &&
                    await _gymUserRoleRepository.GetFirstOrDefaultAsync(gur =>
                        gur.GymId == model.GymId &&
                        gur.UserId == model.FoundUser.UserId &&
                        gur.Role == model.SelectedRole &&
                        (gur.BranchId == (model.SelectedRole == GymRoles.Member ? null : model.BranchId))
                    ) == null)
                {
                    var gymUserRole = new GymUserRole
                    {
                        UserId = model.FoundUser.UserId,
                        Role = model.SelectedRole,
                        GymId = model.GymId,
                        BranchId = model.SelectedRole == GymRoles.Member ? null : model.BranchId
                    };
                    _logger.LogInformation($"Assign POST: Assigned role '{model.SelectedRole}' to userId={model.FoundUser.UserId} for gymId={model.GymId}");
                    await _gymUserRoleRepository.AddAsync(gymUserRole);
                    return RedirectToAction("Dashboard", "Gym", new { gymId = model.GymId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user already has this role in the specified gym or invalid data provided.");
                    _logger.LogWarning($"Assign POST: Failed to assign role '{model.SelectedRole}' to userId={model.FoundUser?.UserId} for gymId={model.GymId} - role may already exist or data is invalid.");
                    return View(model);
                }
            }
            // Default: redisplay form
            return View(model);
        }
    }
}
