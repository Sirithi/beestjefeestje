using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class AnimalController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
    }
}
