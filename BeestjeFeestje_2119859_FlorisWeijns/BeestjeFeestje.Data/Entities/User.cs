using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Entities
{
    public class User : IdentityUser, IValidatableObject
    {
        public string FarmId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public User() : base()
        {
        }

        public User(string email, string farmId, string phoneNumber, string address, string postalCode) : base()
        {
            Email = email;
            UserName = email;
            FarmId = farmId;
            PhoneNumber = phoneNumber;
            Address = address;
            PostalCode = postalCode;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Voeg validaties indien nodig toe
            return new List<ValidationResult>();
        }
    }
}
