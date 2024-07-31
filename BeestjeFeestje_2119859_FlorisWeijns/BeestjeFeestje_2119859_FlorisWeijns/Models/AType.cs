using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class AType
    {
        [Required]
        [Key]
        public string Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

        public AType(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }
}
