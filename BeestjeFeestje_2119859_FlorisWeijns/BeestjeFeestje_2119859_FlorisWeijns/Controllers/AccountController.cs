using Microsoft.AspNetCore.Mvc;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login/5
        [HttpPost]
        public IActionResult Login(string name, string password, string returnUrl)
        {
            return View();
        }

        /*private bool credentialsCorrect(string name, string password)
        {
            if()
        }*/
    }
}
