

using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
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
