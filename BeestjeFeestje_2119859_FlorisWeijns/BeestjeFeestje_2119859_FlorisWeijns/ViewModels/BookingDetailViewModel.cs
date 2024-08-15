using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingDetailViewModel
    {
        public BookingDetailViewModel()
        {
        }
        public BookingDetailViewModel(BookingModel booking)
        {
            Booking = booking;
        }

        public BookingModel Booking { get; set; }
    }
}
