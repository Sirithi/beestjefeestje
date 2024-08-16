using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Utils;
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
        private readonly UserManager<User> _userManager;
        private readonly IBookingRepository _bookingRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IAnimalBookingRepository _animalBookingrepository;
        private readonly IUserRepository _userRepository;
        private readonly IAnimalBookingRepository _animalBookingRepository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IBookingValidator> _validators;

        public BookingService(
            UserManager<User> userManager,
            IBookingRepository bookingRepository,
            IMapper mapper,
            IAnimalBookingRepository animalBookingrepository,
            IAnimalRepository animalRepository,
            IUserRepository userRepository,
            IAnimalBookingRepository animalBookingRepository,
            IEnumerable<IBookingValidator> validators
            )
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _animalBookingrepository = animalBookingrepository;
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _animalBookingRepository = animalBookingRepository;
            _userManager = userManager;
            _validators = validators;
        }

        public Task<BookingModel> Add(BookingModel booking)
        {
            throw new NotImplementedException();
        }

        private async Task<List<ValidationMessage>> ValidateBooking(BookingModel booking)
        {
            var messages = new List<ValidationMessage>();
            foreach (var validator in _validators)
            {
                var result = validator.ValidateBooking(booking);
                messages.Add(result);

                if (!result.Succeeded)
                {
                    return messages;
                }
            }

            return messages;
        }


        public async Task<BookingModel> AddPlaceholder(BookingModel booking)
        {
            var validationResults = await ValidateBooking(booking);
            if(validationResults.Any(result => !result.Succeeded))
            {
                throw new ValidationException(validationResults);
            }

            booking.IsConfirmed = false;
            IEnumerable<Animal> animalEntities = _mapper.Map<IEnumerable<Animal>>(booking.Animals);
            
            booking.Discount = await _CalculateDiscount(booking);
            
            booking.Animals = [];

            foreach(Animal animal in animalEntities)
            {
                AnimalBooking animalBooking= new()
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

        private async Task<double> _CalculateDiscount(BookingModel booking)
        {
            double discount = 0;

            if(booking.Animals.GroupBy(animal => animal.AnimalType).Any(group => group.Count() >= 3))
            {
                discount += 10;
            }

            if(booking.Animals.Any(animal => animal.AnimalName.ToLower().Equals("eend", StringComparison.CurrentCultureIgnoreCase)))
            {
                var randomint = new Random().Next(1, 6);
                if(randomint == 1)
                {
                    discount += 50;
                }
            }

            var kortingdagen = new List<string>() { "maandag", "dinsdag" };
            if(kortingdagen.Contains(booking.Date.Day.ToString().ToLower()))
            {
                discount += 15;
            }

            foreach(char letter in "abcdefghijklmnopqrstuvwxyz")
            {
                if(booking.Animals.Any(animal => animal.Name.ToLower().Contains(letter)))
                {
                    discount += 2;
                }
                else
                {
                    break;
                }
            }

            if(booking.User != null)
            {
                var user = await _userRepository.Get(booking.User.Id);
                if(user != null)
                {
                    var cardRoles = new List<string>() { "silver", "gold", "platinum" };
                    IList<string> roleList = await _userManager.GetRolesAsync(user);
                    IList<string> userRoles = roleList;
                    userRoles = userRoles.Select(role => role.ToLower()).ToList();

                    if(userRoles.Contains("silver") || userRoles.Contains("gold") || userRoles.Contains("platinum"))
                    {
                        discount += 10;
                    }
                }
            }

            if (discount >60)
            {
                discount = 60;
            }
            return discount;
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

        public async Task<IEnumerable<BookingModel>> GetAllWithRelations()
        {
            var result = await _bookingRepository.GetAllWithRelations();
            return _mapper.Map<IEnumerable<BookingModel>>(result);
        }

        public async Task<IEnumerable<BookingModel>> GetByOwnerWithRelations(UserModel user)
        {
            var users = await _userRepository.GetAllByFarm(user.FarmId);
            var ids = users.Select(u => u.Id);
            var result = await _bookingRepository.GetAllByFarmWithRelations(ids);
            return _mapper.Map<IEnumerable<BookingModel>>(result);
        }

        public async Task<IEnumerable<BookingModel>> GetByUser(UserModel user)
        {
            var bookings = await _bookingRepository.GetByUser(_mapper.Map<User>(user));
            return _mapper.Map<IEnumerable<BookingModel>>(bookings);
        }

        public async Task<IEnumerable<BookingModel>> GetByUserWithRelations(UserModel user)
        {
            var bookings = await _bookingRepository.GetByUserWithRelations(_mapper.Map<User>(user));
            return _mapper.Map<IEnumerable<BookingModel>>(bookings);
        }

        public async Task<BookingModel> GetWithRelations(string id)
        {
            var booking = await _bookingRepository.GetWithRelations(id);
            var animalBookings = await _animalBookingRepository.GetByBooking(booking);
            var animals = await _animalRepository.GetAllByIdWithRelations(animalBookings.Select(a=> a.AnimalId));
            booking.Animals = animals;
            return _mapper.Map<BookingModel>(booking);
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
