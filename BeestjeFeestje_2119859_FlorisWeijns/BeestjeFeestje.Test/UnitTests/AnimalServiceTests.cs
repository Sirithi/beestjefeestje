using BeestjeFeestje.Domain.Services;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Test.UnitTests.Utils;
using Moq;
using Xunit;


namespace BeestjeFeestje.Test.UnitTests
{
    public class AnimalServiceTests
    {
        private readonly IAnimalService _animalService;

        public AnimalServiceTests()
        {
            var farmRepo = MockupHelpers.GetFarmRepository();
            var aTypeRepo = MockupHelpers.GetATypeRepository();
            var animalRepo = MockupHelpers.GetAnimalRepository();
            var mapper = MockupHelpers.GetAutoMapper();

            animalRepo.Setup(a => a.Get(It.IsAny<string>())).Returns((string x) => ValueTask.FromResult(EntityHelper.CreateAnimal("1")));

            _animalService = new AnimalService(animalRepo.Object, aTypeRepo.Object, farmRepo.Object, mapper);
        }

        [Fact]
        public async Task GetAnimal()
        {
            // arrange
            var Input = "1";

            // act
            var animal = await _animalService.Get("1");

            // assert
            Xunit.Assert.Equal(animal.Id, Input);
            
        }
    }
}
