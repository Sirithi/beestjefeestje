using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class User : IdentityUser, IValidatableObject
    {
        public string FarmId { get; set; }

        public User() : base()
        {
        }

        public User(string email, string farmId) : base()
        {
            Email = email;
            UserName = email;
            FarmId = farmId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Voeg validaties indien nodig toe
            return new List<ValidationResult>();
        }
    }
}
