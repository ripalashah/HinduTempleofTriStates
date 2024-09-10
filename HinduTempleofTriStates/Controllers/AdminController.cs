using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("ManageUsers");
        }

        // Register a new user
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(new RegisterInputModel());
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "User already exists.");
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Role) && await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return RedirectToAction("ManageUsers");
        }

        // Manage users
        [HttpGet("manage-users")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            // Initialize userViewModels
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                // Ensure userRoles is never null and explicitly convert to List<string>
                var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

                // Ensure user.Id and user.Email are not null
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id ?? string.Empty,  // Ensure no null reference for Id
                    Email = user.Email ?? string.Empty,  // Ensure no null reference for Email
                    UserRoles = userRoles
                });
            }

            // Ensure roles is not null
            var viewModel = new ManageUsersViewModel
            {
                Users = userViewModels,
                Roles = roles ?? new List<IdentityRole>()  // Handle possible null roles
            };

            return View(viewModel);
        }

        // Admin reset another user's password
        [HttpPost("change-password-admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordAdmin(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded) return RedirectToAction("ManageUsers");

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return RedirectToAction("ManageUsers");
        }

        // Manage roles
        [HttpGet("manage-roles")]
        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpPost("manage-roles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(string userId, string roleName, bool addRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (addRole)
                await _userManager.AddToRoleAsync(user, roleName);
            else
                await _userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction("ManageUsers");
        }

        // Manage user roles
        [HttpPost("manage-user-roles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                // Remove user from the role
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                // Add user to the role
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return RedirectToAction("ManageUsers");
        }
    }
}
