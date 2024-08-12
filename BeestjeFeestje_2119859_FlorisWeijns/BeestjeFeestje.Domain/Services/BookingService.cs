using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories;
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
        private readonly IAnimalRepository _animalRepository;
        private readonly IAnimalBookingRepository _animalBookingrepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IAnimalBookingRepository animalBookingrepository, IAnimalRepository animalRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _animalBookingrepository = animalBookingrepository;
            _animalRepository = animalRepository;
        }

        public Task<BookingModel> Add(BookingModel booking)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingModel> AddPlaceholder(BookingModel booking)
        {
            booking.IsConfirmed = false;
            IEnumerable<Animal> animalEntities = _mapper.Map<IEnumerable<Animal>>(booking.Animals);

            booking.Animals = null;

            foreach(Animal animal in animalEntities)
            {
                AnimalBooking animalBooking= new AnimalBooking
                {
                    Id = Guid.NewGuid().ToString(),
                    AnimalId = animal.Id,
                    BookingId = booking.Id
                };
                await _animalBookingrepository.Add(animalBooking);
            }

            var result = await _bookingRepository.Add(_mapper.Map<Booking>(booking));
            return _mapper.Map<BookingModel>(result);
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

        public async Task<IEnumerable<BookingModel>> GetAll()
        {
            var result = await _bookingRepository.GetAll();
            return _mapper.Map<IEnumerable<BookingModel>>(result);
        }

        public async Task<BookingModel> Update(BookingModel booking)
        {
            var bookingEntity = await _bookingRepository.Get(booking.Id);
            bookingEntity.IsConfirmed = booking.IsConfirmed;
            
            await _bookingRepository.Update(bookingEntity);
            var updated_booking = await _bookingRepository.Get(booking.Id);
            return _mapper.Map<BookingModel>(updated_booking);
        }
    }
}
