using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services
{
    public class BookingValidator : IBookingValidator
    {
        Task<ValidationMessage> IBookingValidator.ValidateBooking(BookingModel booking)
        {
            throw new NotImplementedException();
        }
    }
}
