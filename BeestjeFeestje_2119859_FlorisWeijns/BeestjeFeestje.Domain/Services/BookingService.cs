using AutoMapper;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public Task<BookingModel> Add(BookingModel booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingModel> Get(string id)
        {
            var booking = await _bookingRepository.Get(id);
            return _mapper.Map<BookingModel>(booking);
        }

        public Task<IEnumerable<BookingModel>> GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            return Task.FromResult(_mapper.Map<IEnumerable<BookingModel>>(bookings));
        }
    }
}
