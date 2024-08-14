using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FarmId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public UserModel()
        {
        }

        public UserModel(string id, string email, string farmId, string phoneNumber, string address, string postalCode)
        {
            Id = id;
            Email = email;
            FarmId = farmId;
            PhoneNumber = phoneNumber;
            Address = address;
            PostalCode = postalCode;
        }
    }
}
