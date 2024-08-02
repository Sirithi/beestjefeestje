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


        public User() : base()
        {
        }

        public User(string email, string farmId, string phoneNumber) : base()
        {
            Email = email;
            UserName = email;
            FarmId = farmId;
            PhoneNumber = phoneNumber;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Voeg validaties indien nodig toe
            return new List<ValidationResult>();
        }
    }
}
