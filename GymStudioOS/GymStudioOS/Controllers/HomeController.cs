using System.Diagnostics;
using GymStudioOS.Constants;
using GymStudioOS.Models;
using GymStudioOS.Models.Gym.Data;
using GymStudioOS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GymStudioOS.Data;

namespace GymStudioOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Gym> _gymRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IRepository<Gym> gymRepository, UserManager<ApplicationUser> userManager)
        {
            _gymRepository = gymRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("MyGyms");
        }

        [Authorize]
        public async Task<IActionResult> MyGyms()
        {
            var userId = _userManager.GetUserId(User);
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var gyms = (await _gymRepository.GetAllAsync()).Where(g => g.OwnerId == userId).ToList();
            return View(gyms);
        }

        public IActionResult Privacy()
        {
            ViewBag.AppName = AppConstants.APP_NAME;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
