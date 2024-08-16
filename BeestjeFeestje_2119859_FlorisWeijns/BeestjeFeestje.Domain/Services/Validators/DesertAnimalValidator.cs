using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;

namespace BeestjeFeestje.Domain.Services.Validators
{
    public class DesertAnimalValidator : IBookingValidator
    {
        private static readonly string CODE = "DesertAnimalInWinter";
        public async Task<ValidationMessage> ValidateBooking(BookingModel booking)
        {
            var wintermonths = new List<int> { 10, 11, 12, 1, 2 };
            if (wintermonths.Contains(booking.Date.Month))
            {
                if (booking.Animals.Any(a => a.AnimalType.Name.ToLower() == "woestijn"))
                {
                    //string code, string description = "", bool succeeded = false
                    
                    string description = "Een woestijndier kan niet in de winter geboekt worden";
                    return new ValidationMessage(CODE, description, false);
                }
            }
            return new ValidationMessage(CODE, "Succes", true);
        }
    }
}
