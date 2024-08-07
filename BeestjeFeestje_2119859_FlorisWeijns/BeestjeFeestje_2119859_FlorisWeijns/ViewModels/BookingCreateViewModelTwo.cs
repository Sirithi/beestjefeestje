using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingCreateViewModelTwo
    {
        public BookingCreateViewModelTwo()
        {
        }

        public BookingCreateViewModelTwo(BookingCreateViewModelOne modelOne)
        {
            Date = modelOne.Date;
            SelectedAnimals = modelOne.SelectedAnimals;
            Animals = modelOne.Animals;
        }
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public MultiSelectList Animals { get;  set; }
        [Required]
        public IEnumerable<AnimalModel> SelectedAnimals { get; set; }
    }
}
