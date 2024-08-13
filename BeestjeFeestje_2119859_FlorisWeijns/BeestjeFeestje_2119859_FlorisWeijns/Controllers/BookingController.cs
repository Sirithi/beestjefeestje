using AutoMapper;
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
                var modelOne = new BookingCreateViewModelOne(modelTwo);
                return RedirectToAction("BookTwo", modelOne);
            }

            if(modelTwo.SelectedAnimalNames == null)
            {
                ModelState.AddModelError("SelectedAnimalNames", "Please select at least one animal");
                return RedirectToAction("BookTwo", modelTwo);
            }

            BookingCreateViewModelThree model = new(modelTwo);
            model.SelectedAnimals = await _animalService.GetByNames(modelTwo.SelectedAnimalNames);

            User? user = new();

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

            await _bookingService.AddPlaceholder(booking);
            
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
    }
}
