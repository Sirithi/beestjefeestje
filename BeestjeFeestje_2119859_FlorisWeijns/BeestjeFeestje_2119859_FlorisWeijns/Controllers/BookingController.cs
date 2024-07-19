using Microsoft.AspNetCore.Mvc;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Book()
        {
            return View();
        }
    }
}
