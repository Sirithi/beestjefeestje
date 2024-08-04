using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class AnimalCreateViewModel
    {
        public AnimalCreateViewModel()
        {
        }

        [Required, StringLength(60)]
        public string Name { get; set; }
        [Required, StringLength(31)]
        public string AnimalName { get; set; }
        [Required, Range(0,20)]
        public double Cost { get; set; }
        [Required, StringLength(400)]
        public string Description { get; set; }
        public SelectList? ATypesList { get; set; }
        [Required]
        public string SelectedAnimalType { get; set; }

        public AnimalCreateViewModel(IEnumerable<ATypeModel> aTypes)
        {
            ATypesList = new SelectList(aTypes, "Id", "Name");
        }
    }
}
