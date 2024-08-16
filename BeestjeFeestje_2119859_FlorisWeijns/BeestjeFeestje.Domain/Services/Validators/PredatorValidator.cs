using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;

namespace BeestjeFeestje.Domain.Services.Validators
{
    public class PredatorValidator : IBookingValidator
    {
        private static readonly string CODE = "PredatorWithFarmAnimal";
        public async Task<ValidationMessage> ValidateBooking(BookingModel booking)
        {
            if(booking.Animals.Any(animal => animal.AnimalType.Name.ToLower() == "boerderijdier"))
            {
                if(booking.Animals.Any(animal => animal.AnimalName.ToLower() == "ijsbeer") || booking.Animals.Any(animal => animal.AnimalName.ToLower() == "leeuw"))
                {
                    return new ValidationMessage(CODE, "Een boerderijdier kan niet met een roofdier geboekt worden", false);
                }
            }
            return new ValidationMessage(CODE, "Succes", true);
        }
    }
}
