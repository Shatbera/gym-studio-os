using GymStudioOS.Constants;
using GymStudioOS.Data;
using GymStudioOS.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymStudioOS.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly UserManager<ApplicationUser> _users;

        public AccountController(
            SignInManager<ApplicationUser> signIn,
            UserManager<ApplicationUser> users)
        {
            _signIn = signIn;
            _users = users;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _signIn.PasswordSignInAsync(
                vm.Email,
                vm.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(nameof(LoginVm.Password), "Account is locked. Try again later.");
            }
            else
            {
                ModelState.AddModelError(nameof(LoginVm.Password), "Invalid email or password.");
            }

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, EmailConfirmed = true };
            var result = await _users.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _users.AddToRoleAsync(user, AppRoles.Member);
                await _signIn.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var e in result.Errors)
            {
                if (e.Code.Contains("Password", StringComparison.OrdinalIgnoreCase))
                    ModelState.AddModelError(nameof(RegisterVm.Password), e.Description);
                else if (e.Code.Contains("Email", StringComparison.OrdinalIgnoreCase) ||
                         e.Code.Contains("UserName", StringComparison.OrdinalIgnoreCase))
                    ModelState.AddModelError(nameof(RegisterVm.Email), e.Description);
                else
                    ModelState.AddModelError(nameof(RegisterVm.Password), e.Description);
            }

            return View(vm);
        }
    }
}
