using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingCreateViewModelOne
    {
        public BookingCreateViewModelOne()
        {
            SelectedAnimals = new List<AnimalModel>();
        }

        public BookingCreateViewModelOne(BookingCreateViewModelTwo modelTwo)
        {
            Id = modelTwo.Id;
            Name = modelTwo.Name;
            Email = modelTwo.Email;
            PhoneNumber = modelTwo.PhoneNumber;
            Address = modelTwo.Address;
            PostalCode = modelTwo.PostalCode;
            Date = modelTwo.Date;
            SelectedAnimals = modelTwo.SelectedAnimals;
        }

        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public MultiSelectList? Animals { get; set; }

        public IEnumerable<AnimalModel> SelectedAnimals { get; set; }
        
    }
}
