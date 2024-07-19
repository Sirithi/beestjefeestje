using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class AdminViewModel
    {
        public bool IsAdmin { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}
