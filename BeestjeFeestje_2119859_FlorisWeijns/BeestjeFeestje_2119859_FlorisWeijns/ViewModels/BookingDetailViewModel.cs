using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class BookingDetailViewModel
    {
        public BookingDetailViewModel()
        {
        }
        public BookingDetailViewModel(BookingModel booking, double cost, double price)
        {
            Booking = booking;
            Cost = cost;
            Price = price;
        }

        public BookingModel Booking { get; set; }

        public double Cost { get; set; }
        public double Price { get; set; }
    }
}
