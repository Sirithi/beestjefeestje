using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace BeestjeFeestje.Domain.Services.Validators
{
    public class VipValidator : IBookingValidator
    {
        private static readonly string CODE = "UserIsVip";
        private readonly UserManager<User> _userManager;
        public VipValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ValidationMessage> ValidateBooking(BookingModel booking)
        {
            if (booking.Animals.Any(animal => animal.AnimalType.Name.ToLower() == "vip"))
            {
                if (booking.User != null)
                {
                    var roles = await _userManager.GetRolesAsync(booking.User);
                    if (roles.Contains("Platinum"))
                    {
                        return new ValidationMessage(CODE, "succes", true);
                    }
                }
                return new ValidationMessage(CODE, "U mag geen Dieren met het type VIP boeken", false);
            }
            return new ValidationMessage(CODE, "Succes", true);
        }
    }
}
