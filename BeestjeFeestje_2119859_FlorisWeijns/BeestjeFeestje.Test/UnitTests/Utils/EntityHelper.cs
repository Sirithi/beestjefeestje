using BeestjeFeestje.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Test.UnitTests.Utils
{
    internal static class EntityHelper
    {
        public static BeestjeFeestje.Data.Entities.Animal CreateAnimal(string id)
        {
            return new BeestjeFeestje.Data.Entities.Animal
            {
                Id = id,
                Name = "TestAnimal",
                AnimalName = "Penguin",
                Cost = 12.99,
                Description = "Test Description",
                AnimalType = new AType() { Id="1", Name="Sneeuw"},
                FarmId = "1"
            };
        }
    }
}
