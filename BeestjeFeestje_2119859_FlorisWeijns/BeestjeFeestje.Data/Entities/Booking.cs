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
        public int Id { get; set; }
        public User Customer { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
