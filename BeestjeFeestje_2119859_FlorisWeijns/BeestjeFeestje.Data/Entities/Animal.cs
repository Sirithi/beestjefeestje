using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Entities
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(60)]
        public string Name { get; set; }
        [Required, StringLength(31)]
        public string AnimalName { get; set; }
        [Required, MaxLength(20)]
        public double Cost { get; set; }
        [Required, StringLength(400)]
        public string Description { get; set; }
        [Required]
        public AType AnimalType { get; set; }

    }
}
