using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class AnimalType
    {
        [Key, StringLength(30)]
        public string Type { get; set; }
    }
}
