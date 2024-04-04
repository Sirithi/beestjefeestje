using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class Farm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public Farm(string Name)
        {
            this.Name = Name;
        }
    }
}
