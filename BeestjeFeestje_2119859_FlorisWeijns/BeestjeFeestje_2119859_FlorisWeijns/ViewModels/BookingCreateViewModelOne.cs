using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingCreateViewModelOne
    {
        public BookingCreateViewModelOne()
        {
            SelectedAnimalNames = new List<string>();
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
            SelectedAnimalNames = modelTwo.SelectedAnimalNames;
            Animals = modelTwo.Animals;
            SelectedAnimals = modelTwo.SelectedAnimals;
            AnimalList = modelTwo.AnimalList;
            UserId = modelTwo.UserId;
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

        public IEnumerable<string> SelectedAnimalNames { get; set; }
        public IEnumerable<AnimalModel>? SelectedAnimals { get; set; }
        public IEnumerable<AnimalModel>? AnimalList { get; set; }
        public string? UserId { get; set; }
    }
}
