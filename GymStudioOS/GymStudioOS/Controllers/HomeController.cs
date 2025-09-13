using System.Diagnostics;
using GymStudioOS.Constants;
using GymStudioOS.Models;
using GymStudioOS.Models.Gym.Data;
using GymStudioOS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymStudioOS.Controllers
{
    public class HomeController : Controller
    {
    private readonly IRepository<Gym> _gymRepository;

        public HomeController(IRepository<Gym> gymRepository)
        {
            _gymRepository = gymRepository;
        }

        public async Task<IActionResult> Index()
        {
            var gyms = await _gymRepository.GetAllAsync();
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
