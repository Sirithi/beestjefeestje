using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services.Interfaces
{
    public interface IBookingValidator
    {
        Task<ValidationMessage> ValidateBooking(BookingModel booking);
    }
}
