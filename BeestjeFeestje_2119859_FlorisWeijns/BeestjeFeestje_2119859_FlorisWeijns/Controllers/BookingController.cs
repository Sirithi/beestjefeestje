using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;
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
        private readonly IMapper _mapper;

        public BookingController(IAnimalService animalService, IBookingService bookingService, UserManager<User> userManager, IMapper mapper)
        {
            _animalService = animalService;
            _bookingService = bookingService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Admin"))
            {
                var content = new BookingIndexViewModel(await _bookingService.GetAllWithRelations());
                return View(content);
            }
            if(User.IsInRole("User"))
            {
                var user = await _userManager.GetUserAsync(User);
                var userContent = new BookingIndexViewModel(await _bookingService.GetByUserWithRelations(_mapper.Map<UserModel>(user)));
                return View(userContent);
            }
            if(User.IsInRole("Owner"))
            {
                var user = await _userManager.GetUserAsync(User);
                var userContent = new BookingIndexViewModel(await _bookingService.GetByOwnerWithRelations(_mapper.Map<UserModel>(user)));
                return View(userContent);
            }
            return View(new BookingIndexViewModel());
        }

        public async Task<IActionResult> Book()
        {
            var model = new BookingCreateViewModelOne();
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                model.UserId = user.Id;
            }
            model.Date = DateTime.Now.Date;
            model.Id = Guid.NewGuid().ToString();
            return View(model);
        }

        public async Task<IActionResult> BookTwoGet(BookingCreateViewModelTwo modelTwo)
        {
            modelTwo.AnimalList ??= await _animalService.GetAll();
            modelTwo.Animals ??= new MultiSelectList(
                modelTwo.AnimalList.Select(e => e.Name),
                modelTwo.SelectedAnimalNames);

            return View("BookTwo", modelTwo);
        }

        [HttpPost]
        public async Task<IActionResult> BookTwo(BookingCreateViewModelOne modelOne)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Book", modelOne);
            }
            var model = new BookingCreateViewModelTwo(modelOne);
            model.AnimalList ??= await _animalService.GetAll();
            model.Animals ??= new MultiSelectList(
                model.AnimalList.Select(e => e.Name),
                model.SelectedAnimalNames);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookThree(BookingCreateViewModelTwo modelTwo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BookTwoGet", modelTwo);
            }

            BookingCreateViewModelThree model = new(modelTwo)
            {
                SelectedAnimals = await _animalService.GetByNamesWithRelations(modelTwo.SelectedAnimalNames)
            };

            User? user;

            if (model.UserId != null)
            {
                user = await _userManager.FindByIdAsync(model.UserId);
            } else
            {
                user = null;
            }
            
            var booking = new BookingModel
            {
                Id = model.Id,
                Date = model.Date,
                User = user,
                Animals = model.SelectedAnimals,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode
            };
            try
            {
                booking = await _bookingService.AddPlaceholder(booking);
            }
            catch (ValidationException error)
            {
                IEnumerable<ValidationMessage> errors = [];
                foreach (var e in error.ValidationResults)
                {
                    if(e.Succeeded)
                    {
                        continue;
                    }
                    ModelState.AddModelError(e.Code, e.Description);
                }
                return RedirectToAction("BookTwoGet", modelTwo);
            }

            model.Cost = model.SelectedAnimals.Sum(e => e.Cost);
            model.Booking = await _bookingService.GetWithRelations(booking.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(BookingConfirmViewModel model)
        {
            BookingModel booking = await _bookingService.Get(model.Id);
            booking.IsConfirmed = true;
            await _bookingService.Update(booking);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _bookingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string id)
        {
            var booking = await _bookingService.GetWithRelations(id);
            double cost;
            double price;

            if (booking.Animals != null)
            {
                cost = booking.Animals.Sum(e => e.Cost);
            }
            else
            {
                cost = 0;
                price = 0;
            }

            price = Math.Round((cost * (1-(booking.Discount/100))), 2);
            return View(new BookingDetailViewModel(booking, cost, price));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}
