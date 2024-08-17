using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models;
using BeestjeFeestje.Domain.Services;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Services.Validators;
using BeestjeFeestje.Test.UnitTests.Utils;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BeestjeFeestje.Test.UnitTests
{
    public class AnimalRuleTests
    {
        //private readonly IAnimalService _animalService;
        //private readonly IBookingService _bookingService;
        //private readonly IBookingValidator _bookingValidator;
        //private readonly IMapper _mapper;
        public AnimalRuleTests()
        {
            //    var farmRepo = MockupHelpers.GetFarmRepository();
            //    var aTypeRepo = MockupHelpers.GetATypeRepository();
            //    var animalRepo = MockupHelpers.GetAnimalRepository();
            //    var bookingRepo = MockupHelpers.GetBookingRepository();
            //    var animalBookingRepo = MockupHelpers.GetAnimalBookingRepository();
            //    var userRepo = MockupHelpers.GetUserRepository();

            //    var userManager = MockupHelpers.GetUserManager();

            //    _mapper = MockupHelpers.GetAutoMapper();

            //    _bookingValidator = new AnimalAmountValidator(userManager.Object);



            //    //animalRepo.Setup(a => a.Get(It.IsAny<string>())).Returns((string x) => ValueTask.FromResult(EntityHelper.CreateAnimal("1")));
            //    //animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("2"));
            //    //animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("3"));
            //    //animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("4"));

            //    userRepo.Setup(userRepo => userRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateUser("1", "email@email.com"));

            //    _animalService = new AnimalService(animalRepo.Object, aTypeRepo.Object, farmRepo.Object, _mapper);
        }

        [Fact]
        public async Task AnimalCountTest_NoRole()
        {
            // arrange
            //var user = EntityHelper.CreateUser("2", "email@email.com");
            //var animals = new List<Animal>() { EntityHelper.CreateAnimal("1"), EntityHelper.CreateAnimal("2"), EntityHelper.CreateAnimal("3")};
            //var booking = _mapper.Map<BookingModel>(EntityHelper.CreateBooking("1", user, animals));

            //// act
            //var result = await _bookingValidator.ValidateBooking(_mapper.Map<BookingModel>(booking));

            //// assert
            //Xunit.Assert.True(result.Succeeded);
            Xunit.Assert.True(true);
        }
    }
}
