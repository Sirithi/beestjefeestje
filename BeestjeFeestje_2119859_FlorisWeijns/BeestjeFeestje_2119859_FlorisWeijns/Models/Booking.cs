using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public IdentityUser Customer { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, MinLength(1)]
        public ICollection<Animal> Animals { get; set; }

    }
}
