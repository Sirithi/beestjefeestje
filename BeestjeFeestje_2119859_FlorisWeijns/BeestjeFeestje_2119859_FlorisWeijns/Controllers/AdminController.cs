using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BeestjeFeestje_2119859_FlorisWeijns.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BeestjeFeestje.Data.Entities;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ILogger<AdminController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new AdminViewModel { Users = users };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var targetUser = await _userManager.FindByIdAsync(userId);

            

            if (targetUser == null)
            {
                return NotFound();
            }

            var allRoles = await _roleManager.Roles.ToListAsync();
            var roleNames = allRoles.Select(r => r.Name);

            var userRoles = await _userManager.GetRolesAsync(targetUser);

            var possibleRoles = roleNames.Except(userRoles);

            var model = new ManageRolesViewModel
            {
                AvailableRoles = possibleRoles.ToList(),
                UserId = userId,
                AssignedRoles = userRoles.ToList(),
                RolesToAdd = new List<string>(),
                RolesToRemove = new List<string>()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRoles(ManageRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = model.RolesToAdd[0]?.Split(',') ?? new string[] { };
            var rolesToRemove = model.RolesToRemove[0]?.Split(',') ?? new string[] { };

            rolesToRemove = rolesToRemove.Intersect(userRoles).ToArray();
            rolesToAdd = rolesToAdd.Except(userRoles).ToArray();

            if (rolesToAdd.Any())
            {
                IdentityResult result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToRemove.Any())
            {
                IdentityResult result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
