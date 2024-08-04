using BeestjeFeestje.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class FarmViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public ICollection<Animal> Animals { get; set; } = [];
    }
}
