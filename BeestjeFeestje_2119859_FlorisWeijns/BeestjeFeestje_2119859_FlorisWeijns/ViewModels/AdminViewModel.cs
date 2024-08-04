using System.Collections.Generic;
using BeestjeFeestje.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeestjeFeestje_2119859_FlorisWeijns.ViewModels
{
    public class AdminViewModel
    {
        public bool IsAdmin { get; set; }
        public List<User> Users { get; set; }
    }
}
