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
    [Authorize]
    public class AnimalController(BeestjeFeestjeDBContext context, IAnimalService animalService, IAnimalTypeService animalTypeService, UserManager<User> userManager) : Controller
    {
        //private readonly BeestjeFeestjeDBContext _context = context;
        private readonly IAnimalService _animalService = animalService;
        private readonly IAnimalTypeService _animalTypeService = animalTypeService;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            var animals = await _animalService.GetAll();
            
            var viewModel = new AnimalIndexViewModel(animals);

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
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

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
                AnimalType = animalType
            };

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
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
                FarmId = model.FarmId
            };

            await _animalService.Update(animal);
            return RedirectToAction(nameof(Index));
        }


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var animal = await _context.Animals.FindAsync(id);
        //    if (animal == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.AnimalTypes = new SelectList(_context.Types, "Type", "Type", animal.AnimalType);
        //    return View(animal);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AnimalName,Cost,Description,Type")] Animal animal)
        //{
        //    if (id != animal.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(animal);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AnimalExists(animal.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.AnimalTypes = new SelectList(_context.Types, "Type", "Type", animal.AnimalType);
        //    return View(animal);
        //}

        //public async Task<IActionResult> Delete(int? id)
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
