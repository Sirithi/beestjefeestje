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
        public static Animal CreateAnimal(string id)
        {
            return new Animal
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

        public static User CreateUser(string id, string email)
        {
            return new User
            {
                Id = id,
                UserName = "TestUser",
                Email = email
            };
        }

        public static Booking CreateBooking(string id, User user, IList<Animal> animals)
        {
            return new Booking
            {
                Id = id,
                Date = DateTime.Now,
                Animals = animals,
                User = user,
                Name = "TestName",
                Email = ""
            };
        }
    }
}
