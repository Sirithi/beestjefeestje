using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        BeestjeFeestjeDBContext context
        ) : Controller
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;
        private readonly BeestjeFeestjeDBContext _context = context;
        

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(InputModel Input)
        {
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(Input.FarmName))
                {
                    ModelState.AddModelError(string.Empty, "Farm name is required.");
                    return View(Input);
                }

                Farm farm = new(Input.FarmName);
                _context.Farms.Add(farm);
                await _context.SaveChangesAsync();

                var user = new User { UserName = Input.Email, Email = Input.Email, FarmId = farm.Id, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Input);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            var adminClaim = new Claim(ClaimTypes.Role, "Admin");
                            await _userManager.AddClaimAsync(user, adminClaim);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
