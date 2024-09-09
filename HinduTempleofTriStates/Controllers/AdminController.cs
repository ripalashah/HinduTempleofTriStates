using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /admin/index
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View(); // Admin dashboard
        }

        // GET: /admin/register-user
        [HttpGet("register-user")]
        public IActionResult RegisterUser()
        {
            return View(); // View for registering a user
        }

        // POST: /admin/register-user
        [HttpPost("register-user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                await _userManager.AddToRoleAsync(user, model.Role);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
