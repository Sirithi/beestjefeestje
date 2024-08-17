using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace BeestjeFeestje.Domain.Services.Validators
{
    public class AnimalAmountValidator : IBookingValidator
    {
        private static readonly string CODE = "BookingAnimalCount";
        private readonly UserManager<User> _userManager;

        public AnimalAmountValidator()
        {
        }

        public AnimalAmountValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ValidationMessage> ValidateBooking(BookingModel booking)
        {
            if(booking.User != null)
            {
                var roles = await _userManager.GetRolesAsync(booking.User);
                if (roles.Contains("Admin") || roles.Contains("Platinum"))
                {
                    return new ValidationMessage(CODE, "Succes", true);
                }
                if (roles.Contains("Silver"))
                {
                    if (booking.Animals.Count() > 4)
                    {
                        return new ValidationMessage(CODE, "U kunt maximaal 4 dieren boeken", false);
                    }
                }
            }
            if(booking.Animals.Count() > 3)
            {
                return new ValidationMessage(CODE, "U kunt maximaal 3 dieren boeken", false);
            }
            return new ValidationMessage(CODE, "Succes", true);
        }
    }
}
