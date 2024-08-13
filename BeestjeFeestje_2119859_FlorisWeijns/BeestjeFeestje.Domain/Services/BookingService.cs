using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IAnimalBookingRepository animalBookingrepository, IAnimalRepository animalRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _animalBookingrepository = animalBookingrepository;
            _animalRepository = animalRepository;
            _userRepository = userRepository;
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

        public async Task<bool> Delete(string id)
        {
            Booking booking = await _bookingRepository.Get(id);
            if (booking == null)
            {
                return false;
            }
            await _bookingRepository.Delete(booking);
            return true;
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

        public async Task<IEnumerable<BookingModel>> GetByUser(UserModel user)
        {
            var bookings = await _bookingRepository.GetByUser(_mapper.Map<User>(user));
            return _mapper.Map<IEnumerable<BookingModel>>(bookings);
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
