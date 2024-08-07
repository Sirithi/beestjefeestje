using BeestjeFeestje.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }
        public User Customer { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
