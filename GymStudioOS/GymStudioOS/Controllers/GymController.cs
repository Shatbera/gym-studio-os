using Microsoft.AspNetCore.Mvc;
using GymStudioOS.Models.Gym.View;
using GymStudioOS.Repositories;
using GymStudioOS.Models.Gym.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GymStudioOS.Data;

namespace GymStudioOS.Controllers
{
	public class GymController : Controller
	{
		private readonly IRepository<Gym> _gymRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public GymController(IRepository<Gym> gymRepository, UserManager<ApplicationUser> userManager)
        {
            _gymRepository = gymRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public IActionResult CreateGym()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> CreateGym(GymVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var ownerId = _userManager.GetUserId(User);
            if (ownerId == null)
            {
                ModelState.AddModelError(string.Empty, "OwnerId cannot be null. Please ensure you are logged in.");
                return View(model);
            }
            var gym = new Gym
            {
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                OwnerId = ownerId
            };
            await _gymRepository.AddAsync(gym);
            return RedirectToAction("Index", "Home");
        }
	}
}
