using BeestjeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Test.UnitTests.Utils
{
    internal class ModelHelper
    {
        public static AnimalModel GetAnimalModel()
        {
            return new AnimalModel
            {
                Id = "1",
                Name = "TestName",
                AnimalName = "TestAnimalName",
                Cost = 10.0,
                Description = "TestDescription",
                AnimalType = new ATypeModel() { Id="1", Name="Sneeuw"},
                FarmId = "1"
            };
        }
    }
}
