using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Entities
{
    public class AnimalBooking
    {
        public int Id { get; set; }
        public Animal Animal { get; set; }
        public Booking Booking { get; set; }
    }
}
