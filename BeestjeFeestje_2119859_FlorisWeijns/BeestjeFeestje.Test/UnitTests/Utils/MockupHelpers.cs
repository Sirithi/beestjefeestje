using AutoMapper;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models.Map;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BeestjeFeestje.Test.UnitTests.Utils
{
    internal static class MockupHelpers
    {
        public static Mock<IATypeRepository> GetATypeRepository() => new();
        
        public static Mock<IAnimalRepository> GetAnimalRepository() => new();

        public static Mock<IFarmRepository> GetFarmRepository() => new();

        public static Mock<IBookingRepository> GetBookingRepository() => new();

        public static Mock<IAnimalBookingRepository> GetAnimalBookingRepository() => new();

        public static Mock<IUserRepository> GetUserRepository() => new();

        public static IMapper GetAutoMapper()
        {
            return new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        }
    }
}
