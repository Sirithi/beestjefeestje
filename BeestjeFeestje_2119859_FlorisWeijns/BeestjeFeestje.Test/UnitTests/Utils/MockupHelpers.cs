using AutoMapper;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Domain.Models.Map;
using BeestjeFeestje.Domain.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Test.UnitTests.Utils
{
    internal static class MockupHelpers
    {
        public static Mock<IATypeRepository> GetATypeRepository() => new Mock<IATypeRepository>();
        
        public static Mock<IAnimalRepository> GetAnimalRepository() => new Mock<IAnimalRepository>();

        public static Mock<IFarmRepository> GetFarmRepository() => new Mock<IFarmRepository>();

        public static IMapper GetAutoMapper()
        {
            return new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        }
    }
}
