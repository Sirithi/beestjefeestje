using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;

namespace BeestjeFeestje.Domain.Services.Validators
{
    public class ArcticAnimalValidator : IBookingValidator
    {
        private static readonly string CODE = "ArcticAnimalInSummer";
        public async Task<ValidationMessage> ValidateBooking(BookingModel booking)
        {
            var wintermonths = new List<int> { 6, 7, 8 };
            if (wintermonths.Contains(booking.Date.Month))
            {
                if (booking.Animals.Any(a => a.AnimalType.Name.ToLower() == "sneeuw"))
                {
                    string description = "Een woestijndier kan niet in de winter geboekt worden";
                    return new ValidationMessage(CODE, description, false);
                }
            }
            return new ValidationMessage(CODE, "Succes", true);
        }
    }
}
