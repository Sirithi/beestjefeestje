using BeestjeFeestje_2119859_FlorisWeijns.Data;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BeestjeFeestjeDBContext _context;


        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            BeestjeFeestjeDBContext beestjeFeestjeContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = beestjeFeestjeContext;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel Input) 
        {
            if (ModelState.IsValid)
            {
                var farm = new Farm(Input.FarmName);
                //var user = new User { UserName = Input.Email, Email = Input.Email, FarmId = farm.Id };
                var user = new User(Input.Email, farm.Id);
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                _context.Farms.Add(farm);
                _context.SaveChanges();
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
            return View(); 
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel Input)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
