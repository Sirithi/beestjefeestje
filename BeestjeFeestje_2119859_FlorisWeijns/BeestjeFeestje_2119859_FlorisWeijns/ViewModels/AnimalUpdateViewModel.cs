using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class AnimalUpdateViewModel
    {
        public AnimalUpdateViewModel()
        {
        }
        [Required]
        public string Id { get; set; }
        [Required, StringLength(60)]
        public string Name { get; set; }
        [Required, StringLength(31)]
        public string AnimalName { get; set; }
        [Required, Range(0, 20)]
        public double Cost { get; set; }
        [Required, StringLength(400)]
        public string Description { get; set; }
        public SelectList? ATypesList { get; set; }
        [Required]
        public string SelectedAnimalType { get; set; }
        [Required]
        public string FarmId { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public AnimalUpdateViewModel(IEnumerable<ATypeModel> aTypes)
        {
            ATypesList = new SelectList(aTypes, "Id", "Name");
        }

        public AnimalUpdateViewModel(string id, string name, string animalName, double cost, string description, string farmId, IEnumerable<ATypeModel> aTypesList, string selectedAnimalType)
        {
            Id = id;
            Name = name;
            AnimalName = animalName;
            Cost = cost;
            Description = description;
            FarmId = farmId;
            ATypesList = new SelectList(aTypesList, "Id", "Name");
            SelectedAnimalType = selectedAnimalType;
        }

        public AnimalUpdateViewModel(AnimalModel animal, IEnumerable<ATypeModel> aTypes)
        {
            Id = animal.Id;
            Name = animal.Name;
            AnimalName = animal.AnimalName;
            Cost = animal.Cost;
            Description = animal.Description;
            FarmId = animal.FarmId;
            ATypesList = new SelectList(aTypes, "Id", "Name");
            SelectedAnimalType = animal.AnimalType.Id;
        }
    }
}
