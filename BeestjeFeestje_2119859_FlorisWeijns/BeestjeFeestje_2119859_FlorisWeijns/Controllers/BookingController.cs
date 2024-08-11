using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje_2119859_FlorisWeijns.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IBookingService _bookingService;
        private readonly UserManager<User> _userManager;

        public BookingController(IAnimalService animalService, IBookingService bookingService, UserManager<User> userManager)
        {
            _animalService = animalService;
            _bookingService = bookingService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var bookings = new BookingIndexViewModel(new List<BookingModel>());

            return View(bookings);
        }

        public async Task<IActionResult> Book()
        {
            var model = new BookingCreateViewModelOne();
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                model.User = user;
            }
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
            if(model.AnimalList == null)
            {
                model.AnimalList = await _animalService.GetAll();
            }
            if (model.Animals == null)
            {
                model.Animals = new MultiSelectList(
                model.AnimalList.Select(e => e.Name),
                model.SelectedAnimalNames);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookThree(BookingCreateViewModelTwo modelTwo)
        {
            if (!ModelState.IsValid)
            {
                var modelOne = new BookingCreateViewModelOne(modelTwo);
                return RedirectToAction("BookTwo", modelOne);
            }

            if(modelTwo.SelectedAnimalNames == null)
            {
                ModelState.AddModelError("SelectedAnimalNames", "Please select at least one animal");
                return RedirectToAction("BookTwo", modelTwo);
            }

            var model = new BookingCreateViewModelThree(modelTwo);
            if (model.SelectedAnimalNames.Count() == 1)
            {
                
                var toAppend = await _animalService.GetByName(model.SelectedAnimalNames.First());
                model.SelectedAnimals = new List<AnimalModel>() { toAppend };
                //model.SelectedAnimals.Append(toAppend);
            }
            else
            {
                model.SelectedAnimals = await _animalService.GetByNames(modelTwo.SelectedAnimalNames);
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateViewModelThree model)
        {
            if(!ModelState.IsValid)
            {
                var modelTwo = new BookingCreateViewModelTwo(model);
                return RedirectToAction("BookThree", modelTwo);
            }

            if (model.SelectedAnimals == null || model.SelectedAnimals.Count() == 0)
            {
                if(model.SelectedAnimalNames == null)
                {
                    ModelState.AddModelError("SelectedAnimalNames", "Please select at least one animal");
                    return RedirectToAction("BookTwo", model);
                }
                else
                {
                    //var names = model.SelectedAnimalNames;
                    var names = model.SelectedAnimalNames;
                    //var animals = await _animalService.GetByNames(names);
                    //model.SelectedAnimals = animals.ToList();
                }
            }

            var booking = new BookingModel
            {
                Id = model.Id,
                Date = model.Date,
                User = model.User,
                Animals = model.SelectedAnimals,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode
            };
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookingCreateViewModelOne model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Animals == null)
                {
                    var animals = await _animalService.GetAll();
                    model.Animals = new MultiSelectList(
                        animals.Select(e => e.Name),
                        model.SelectedAnimalNames);
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
