using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Entities
{
    public class Farm
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string FarmName { get; set; }

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public Farm(string farmName) : base()
        {
            Id = Guid.NewGuid().ToString();
            FarmName = farmName;
        }


    }
}
