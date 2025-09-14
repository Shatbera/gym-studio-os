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
        private readonly IRepository<Class> _classRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GymController(IRepository<Gym> gymRepository, IRepository<Class> classRepository, UserManager<ApplicationUser> userManager)
        {
            _gymRepository = gymRepository;
            _classRepository = classRepository;
            _userManager = userManager;
        }



        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> MyGyms()
        {
            var userId = _userManager.GetUserId(User);
            var gyms = (await _gymRepository.GetAllAsync()).Where(g => g.OwnerId == userId).ToList();
            return View(gyms);
        }


        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> Dashboard(int gymId)
        {
            var gym = await _gymRepository.GetByIdAsync(gymId);
            var userId = _userManager.GetUserId(User);

            var isAdmin = User.IsInRole(AppRoles.Admin);
            if (gym.OwnerId != userId && !isAdmin)
            {
                return Forbid();
            }

            return View(new GymDashboardVm
            {
                Gym = gym,
                Classes = (await _classRepository.GetAllAsync()).Where(c => c.GymId == gymId).ToList()
            });
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


        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public IActionResult EditClass(int gymId)
        {
            ViewBag.GymId = gymId;
            return View();
        }




        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> CreateClass(ClassVm model, int gymId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var classEntity = new Class
            {
                Name = model.Name,
                DefaultDurationMinutes = model.DefaultDurationMinutes,
                GymId = gymId
            };
            await _classRepository.AddAsync(classEntity);
            return RedirectToAction("Dashboard", "Gym", new { gymId });
        }


        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> EditClass(ClassVm model, int gymId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var classEntity = new Class
            {
                Id = model.Id,
                Name = model.Name,
                DefaultDurationMinutes = model.DefaultDurationMinutes,
                GymId = gymId
            };
            await _classRepository.UpdateAsync(classEntity);
            return RedirectToAction("Dashboard", "Gym", new { gymId });
        }


    }
}
