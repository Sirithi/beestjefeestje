using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingCreateViewModelThree
    {
        public BookingCreateViewModelThree()
        {
        }

        public BookingCreateViewModelThree(BookingCreateViewModelTwo modelTwo)
        {
            Id = modelTwo.Id;
            Date = modelTwo.Date;
            SelectedAnimalNames = modelTwo.SelectedAnimalNames;
            Animals = modelTwo.Animals;
            SelectedAnimals = modelTwo.SelectedAnimals;
            AnimalList = modelTwo.AnimalList;
            UserId = modelTwo.UserId;
            Email = modelTwo.Email;
            Name = modelTwo.Name;
            PhoneNumber = modelTwo.PhoneNumber;
            Address = modelTwo.Address;
            PostalCode = modelTwo.PostalCode;
        }

        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public MultiSelectList? Animals { get;  set; }
        [Required, MinLength(1)]
        public IEnumerable<string> SelectedAnimalNames { get; set; }
        public IEnumerable<AnimalModel>? SelectedAnimals { get; set; }
        public IEnumerable<AnimalModel>? AnimalList { get; set; }
        public string? UserId { get; set; }
        public double Cost { get; set; }
        public BookingModel Booking { get; set; }
        public double Price { get { return Math.Round(Cost * ((100 - Booking.Discount) / 100), 2); }}
    }
}
