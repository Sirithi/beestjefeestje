﻿using BeestjeFeestje.Domain.Models;
using Microsoft.AspNetCore.Identity;
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
            Id = modelOne.Id;
            Date = modelOne.Date;
            SelectedAnimalNames = modelOne.SelectedAnimalNames;
            Animals = modelOne.Animals;
            SelectedAnimals = modelOne.SelectedAnimals;
            AnimalList = modelOne.AnimalList;
            User = modelOne.User;
            Name = modelOne.Name;
            Email = modelOne.Email;
            PhoneNumber = modelOne.PhoneNumber;
            Address = modelOne.Address;
            PostalCode = modelOne.PostalCode;
        }
        public BookingCreateViewModelTwo(
            string id, 
            string? name, 
            string? email, 
            string? phoneNumber, 
            string? address, 
            string? postalCode, 
            DateTime date, 
            MultiSelectList? animals, 
            IEnumerable<string> selectedAnimalNames, 
            IEnumerable<AnimalModel>? selectedAnimals, 
            IEnumerable<AnimalModel> animalList, 
            IdentityUser? user)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PostalCode = postalCode;
            Date = date;
            Animals = animals;
            SelectedAnimalNames = selectedAnimalNames;
            SelectedAnimals = selectedAnimals;
            AnimalList = animalList;
            User = user;
        }

        public BookingCreateViewModelTwo(BookingCreateViewModelThree modelThree)
        {
            Id = modelThree.Id;
            Date = modelThree.Date;
            SelectedAnimalNames = modelThree.SelectedAnimalNames;
            Animals = modelThree.Animals;
            SelectedAnimals = modelThree.SelectedAnimals;
            AnimalList = modelThree.AnimalList;
            User = modelThree.User;
            Name = modelThree.Name;
            Email = modelThree.Email;
            PhoneNumber = modelThree.PhoneNumber;
            Address = modelThree.Address;
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
        [Required]
        public IEnumerable<string> SelectedAnimalNames { get; set; }
        public IEnumerable<AnimalModel>? SelectedAnimals { get; set; }
        public IEnumerable<AnimalModel>? AnimalList { get; set; }
        public IdentityUser? User { get; set; }

    }
}
