using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: /account/login
        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            var model = new LoginInputModel();
            return View(model);
        }

        // POST: /account/login
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /account/register (Restricted to Admins)
        [HttpGet("register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            var model = new RegisterInputModel();
            return View(model);
        }

        // POST: /account/register (Restricted to Admins)
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admins can register new users
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Register";
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.Role) && await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewData["Title"] = "Register";
            return View(model);
        }

        // GET: /account/manage-roles (Restricted to Admins)
        [HttpGet("manage-roles")]
        [Authorize(Roles = "Admin")]
        public IActionResult ManageRoles()
        {
            return View();
        }

        // POST: /account/manage-roles (Restricted to Admins)
        [HttpPost("manage-roles")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(string userId, string roleName, bool addRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (addRole)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            return RedirectToAction("ManageRoles");
        }

        // POST: /account/logout
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Admin-Only Page
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyPage()
        {
            return View();
        }

        // Accountant and Admin-Only Page
        [HttpGet("accountant-page")]
        [Authorize(Roles = "Admin,Accountant")]
        public IActionResult AccountantPage()
        {
            return View();
        }

        // Counter-Only Page
        [HttpGet("counter-page")]
        [Authorize(Roles = "Counter")]
        public IActionResult CounterPage()
        {
            return View();
        }
    }
}
