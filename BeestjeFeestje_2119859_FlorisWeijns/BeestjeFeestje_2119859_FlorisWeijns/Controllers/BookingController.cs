using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje_2119859_FlorisWeijns.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAnimalService _animalService;

        public BookingController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public IActionResult Index()
        {
            var bookings = new BookingIndexViewModel(new List<BookingModel>());

            return View(bookings);
        }

        public IActionResult Book() {
            var model = new BookingCreateViewModelOne();
            model.Date = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> BookTwo(BookingCreateViewModelOne modelOne)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Book", modelOne);
            }
            var model = new BookingCreateViewModelTwo(modelOne);
            var animals = await _animalService.GetAll();
            model.Animals = new MultiSelectList(
                animals.Select(e => e.Name),
                model.SelectedAnimals);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookThree(BookingCreateViewModelTwo modelTwo)
        {
            Console.WriteLine("Premodelstate");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelstate");
                var modelOne = new BookingCreateViewModelOne(modelTwo);
                return RedirectToAction("BookTwo", modelOne);
            }
            Console.WriteLine("PostModelState");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookingCreateViewModelOne model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }   
    }
}
