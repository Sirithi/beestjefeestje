using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Entities
{
    public class Booking
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public IEnumerable<Animal> Animals { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
