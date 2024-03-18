using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [StringLength(45)]
        public required string UserName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        [RegularExpression("^[1-9][0-9]{3}(?!sa|sd|ss)(?:[A-Z]{2})$", ErrorMessage = "Dit is geen geldige Postcode")]
        public required string Postcode
        { get; set; }
        [MaxLength(5)]
        public required int HouseNumber { get; set; }
        [StringLength(3)]
        public string? Addage { get; set; }
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
