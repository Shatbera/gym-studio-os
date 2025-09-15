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
        public async Task<IActionResult> EditClass(int gymId, int classId)
        {
            ViewBag.GymId = gymId;
            if (classId == 0)
            {
                return View();
            }
            var classEntity = await _classRepository.GetByIdAsync(classId);
            if (classEntity == null || classEntity.GymId != gymId)
            {
                return NotFound();
            }
            var model = new ClassVm
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                DefaultDurationMinutes = classEntity.DefaultDurationMinutes,
            };
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> CreateClass(ClassVm model, int gymId)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                }
                Console.WriteLine($"GymId: {gymId}");
                return View("EditClass", model);
            }
            Console.WriteLine($"Creating class: Name={model.Name}, Duration={model.DefaultDurationMinutes}, GymId={gymId}");
            Console.WriteLine($"GymId: {gymId}");
            var classEntity = new Class
            {
                Name = model.Name,
                DefaultDurationMinutes = model.DefaultDurationMinutes,
                GymId = gymId
            };
            await _classRepository.AddAsync(classEntity);
            Console.WriteLine("Class created successfully.");
            return RedirectToAction("Dashboard", "Gym", new { gymId });
        }


        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> EditClass(ClassVm model, int gymId)
        {
            Console.WriteLine($"EditClass called with: Id={model.Id}, Name={model.Name}, Duration={model.DefaultDurationMinutes}, GymId={gymId}");
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                }
                Console.WriteLine("ModelState is invalid. Returning view with model.");
                return View(model);
            }
            var classEntity = new Class
            {
                Id = model.Id,
                Name = model.Name,
                DefaultDurationMinutes = model.DefaultDurationMinutes,
                GymId = gymId
            };
            Console.WriteLine($"Updating class entity: Id={classEntity.Id}, Name={classEntity.Name}, Duration={classEntity.DefaultDurationMinutes}, GymId={classEntity.GymId}");
            await _classRepository.UpdateAsync(classEntity);
            Console.WriteLine("Class updated successfully.");
            return RedirectToAction("Dashboard", "Gym", new { gymId });
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.OwnerOrAdmin)]
        public async Task<IActionResult> DeleteClass(int classId, int gymId)
        {
            await _classRepository.DeleteAsync(classId);
            return RedirectToAction("Dashboard", "Gym", new { gymId });
        }
    }
}
