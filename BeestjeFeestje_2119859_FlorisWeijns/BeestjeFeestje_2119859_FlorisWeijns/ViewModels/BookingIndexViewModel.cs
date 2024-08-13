using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingIndexViewModel
    {
        public BookingIndexViewModel()
        {

        }

        public BookingIndexViewModel(IEnumerable<BookingModel> bookings, User user)
        {
            Bookings = bookings;
            User = user;
        }

        public BookingIndexViewModel(IEnumerable<BookingModel> bookings)
        {
            Bookings = bookings;
        }

        public IEnumerable<BookingModel>? Bookings { get; set; }
        public User? User { get; set; }
    }
}
