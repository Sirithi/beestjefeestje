using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BeestjeFeestje_2119859_FlorisWeijns.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BeestjeFeestje.Data.Entities;
using System.Security.Claims;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    [Authorize(Roles = "Admin, Owner")]
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ILogger<AdminController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            var users = await _getUsers(User);
            var model = new AdminViewModel { Users = users.ToList() };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var targetUser = await _userManager.FindByIdAsync(userId);
            var currentUser = await _userManager.GetUserAsync(User);
            if (targetUser == null)
            {
                return NotFound();
            }

            var allRoles = await _roleManager.Roles.ToListAsync();
            var roleNames = allRoles.Select(r => r.Name);

            var userRoles = await _userManager.GetRolesAsync(targetUser);

            if (!User.IsInRole("Admin"))
            {
                roleNames.Except(["Admin"]);
            }

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

            var rolesToAdd = model.RolesToAdd[0]?.Split(',') ?? [];
            var rolesToRemove = model.RolesToRemove[0]?.Split(',') ?? [];

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

        public async Task<IActionResult> CreateUser()
        {
            var model = new CreateUserViewModel();
            model.Id = Guid.NewGuid().ToString();
            User? owner = (await _userManager.GetUserAsync(User));
            if (owner == null)
            {
                return View(model);
            }
            model.FarmId = owner.FarmId;
            return View(model);
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FarmId = model.FarmId,
                    PhoneNumber = model.PhoneNumber,
                    PostalCode = model.PostalCode,
                    Address = model.Address,
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    IdentityResult roleResult = await _userManager.AddToRolesAsync(newUser, ["User"]);
                    return RedirectToAction(nameof(ViewCredentials), model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult ViewCredentials(CreateUserViewModel model)
        {
            return View(model);
        }

        private async Task<IEnumerable<User>> _getUsers(ClaimsPrincipal user)
        {
            if (User.IsInRole("Admin"))
            {
                return await _userManager.Users.ToListAsync();
            }

            var adminUser = await _userManager.GetUserAsync(user);
            var users = await _userManager.Users.Where(u => u.FarmId == adminUser.FarmId).ToListAsync();
            return users;
        }
    }
}
