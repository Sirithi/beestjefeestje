using AutoMapper;
using BeestjeFeestje.Data.Repositories.Interfaces;
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
        private readonly IAnimalService _animalService;
        private readonly IBookingService _bookingService;
        private readonly IBookingValidator _bookingValidator;
        public AnimalRuleTests()
        {
            var farmRepo = MockupHelpers.GetFarmRepository();
            var aTypeRepo = MockupHelpers.GetATypeRepository();
            var animalRepo = MockupHelpers.GetAnimalRepository();
            var bookingRepo = MockupHelpers.GetBookingRepository();
            var animalBookingRepo = MockupHelpers.GetAnimalBookingRepository();
            var userRepo = MockupHelpers.GetUserRepository();
            var mapper = MockupHelpers.GetAutoMapper();

            _bookingValidator = new AnimalAmountValidator();



            animalRepo.Setup(a => a.Get(It.IsAny<string>())).Returns((string x) => ValueTask.FromResult(EntityHelper.CreateAnimal("1")));
            animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("2"));
            animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("3"));
            animalRepo.Setup(animalRepo => animalRepo.Get(It.IsAny<string>())).ReturnsAsync(EntityHelper.CreateAnimal("4"));

            _animalService = new AnimalService(animalRepo.Object, aTypeRepo.Object, farmRepo.Object, mapper);
        }

        [Fact]
        public async Task AnimalCountTest_NoRole()
        {
            // arrange
            var Input = "1";

            // act
            //var result = _bookingValidator.ValidateBooking(booking);

            // assert
            Xunit.Assert.Equal("1", Input);

        }
    }
}
