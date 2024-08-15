using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [StringLength(36, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 36)]
        public string FarmId { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        
    }
}
