using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var targetUser = await _userManager.FindByIdAsync(userId);

            if (targetUser == null)
            {
                return NotFound();
            }

            var model = new ManageRolesViewModel
            {
                UserId = targetUser.Id,
            };

            var targetUserRoles = await _userManager.GetRolesAsync(targetUser);
            var allRoles = _roleManager.Roles.ToList();

            var userRoles = new List<IdentityRole>();

            foreach (var role in allRoles)
            {
                if(await _userManager.IsInRoleAsync(targetUser, role.Name))
                {
                    userRoles.Add(role);
                    model.SelectedRoles.Add(role);
                }
                else
                {
                    model.PossibleRoles.Add(role);
                }
            }

            ViewBag.Roles = new SelectList(model.PossibleRoles, "Name", "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(string userId, ManageRolesViewModel model)
        {
            var targetUser = await _userManager.FindByIdAsync(userId);

            if (targetUser == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(targetUser);
            var rolesToAdd = model.SelectedRoles.Select(r => r.Name).Except(currentRoles);
            //var rolesToRemove = currentRoles.Except(model.roles);

            await _userManager.AddToRolesAsync(targetUser, rolesToAdd);
            //await _userManager.RemoveFromRolesAsync(targetUser, rolesToRemove);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
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
