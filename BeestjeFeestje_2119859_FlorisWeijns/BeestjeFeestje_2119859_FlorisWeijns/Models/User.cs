using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class User : IdentityUser, IValidatableObject
    {
        public int FarmId { get; set; }

        public User(string Email, int farmId) : base(Email)
        {
            FarmId = farmId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
