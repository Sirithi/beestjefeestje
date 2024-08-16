using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Utils
{
    public class ValidationMessage
    {
        public string Code { get; }
        public string Description { get; }
        public bool Succeeded { get; }

        public ValidationMessage()
        {
            Code = "ValidationError";
            Description = "Er is een fout opgetreden";
            Succeeded = false;
        }

        public ValidationMessage(string code, string description = "", bool succeeded = false) : this()
        {
            Code = code;
            Description = description;
            Succeeded = succeeded;
        }

        public ValidationMessage(IdentityError error) : this()
        {
            Code = error.Code;
            Description = error.Description;
            Succeeded = false;
        }


    }
}
