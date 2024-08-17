using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje_2119859_FlorisWeijns.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje_2119859_FlorisWeijns.Controllers
{
    [Authorize(Roles = "Owner")]
    public class AnimalController(IAnimalService animalService, IAnimalTypeService animalTypeService, UserManager<User> userManager) : Controller
    {
        private readonly IAnimalService _animalService = animalService;
        private readonly IAnimalTypeService _animalTypeService = animalTypeService;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            var animals = await _animalService.GetAll();
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToPage("/Home/Index", new { area = "Identity" });
            }
            if(User.IsInRole("Admin"))
            {
                var adminAnimals = new AnimalIndexViewModel(animals);
                return View(adminAnimals);
            }
            var viewModel = new AnimalIndexViewModel(animals.Where(animal => animal.FarmId == user.FarmId));

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var types = await _animalTypeService.GetAll();

            AnimalCreateViewModel model = new AnimalCreateViewModel(types);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var animalType = await _animalTypeService.GetById(model.SelectedAnimalType);

            var animal = new AnimalModel()
            {
                Name = model.Name,
                AnimalName = model.AnimalName,
                Cost = model.Cost,
                Description = model.Description,
                AnimalType = animalType,
                ImageUrl = model.ImageUrl
            };

            var userId = _userManager.GetUserId(User);
            if(userId == null)
            {
                return RedirectToAction("Index");
            }
            User? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                   return RedirectToAction("Index");
            }
            var farmId = user.FarmId;

            try
            {
                var result = await _animalService.Add(animal, farmId);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _animalService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var animal = await _animalService.Get(id);
            var types = await _animalTypeService.GetAll();

            AnimalUpdateViewModel model = new AnimalUpdateViewModel(animal, types);

            return View(model);
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalUpdateViewModel model)
        {
            var animalType = await _animalTypeService.GetById(model.SelectedAnimalType);

            var animal = new AnimalModel()
            {
                Id = model.Id,
                Name = model.Name,
                AnimalName = model.AnimalName,
                Cost = model.Cost,
                Description = model.Description,
                AnimalType = animalType,
                FarmId = model.FarmId,
                ImageUrl = model.ImageUrl
            };

            await _animalService.Update(animal);
            return RedirectToAction(nameof(Index));
        }


        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var animal = await _context.Animals
        //        .Include(a => a.AnimalType)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (animal == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(animal);
        //}

        //private bool AnimalExists(int id)
        //{
        //    return _context.Animals.Any(e => e.Id == id);
        //}
    }
}
