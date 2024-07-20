using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new AdminViewModel { Users = users };

            return View(model);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                SelectedRoles = []
            };

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

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
            var rolesToAdd = model.SelectedRoles.Except(currentRoles);
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
