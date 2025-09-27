using GymStudioOS.Data;
using GymStudioOS.Models.Gym.Data;
using GymStudioOS.Models.Gym.View;
using GymStudioOS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymStudioOS.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IRepository<UserProfile> userProfileRepository, UserManager<ApplicationUser> userManager, ILogger<UserProfileController> logger)
        {
            _userProfileRepository = userProfileRepository;
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
    }
}
