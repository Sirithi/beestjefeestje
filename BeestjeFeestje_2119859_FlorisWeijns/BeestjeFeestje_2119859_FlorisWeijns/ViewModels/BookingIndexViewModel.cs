using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingIndexViewModel
    {
        public BookingIndexViewModel(IEnumerable<BookingModel> bookings)
        {
            Bookings = bookings;
        }

        public IEnumerable<BookingModel>? Bookings { get; set; }
    }
}
