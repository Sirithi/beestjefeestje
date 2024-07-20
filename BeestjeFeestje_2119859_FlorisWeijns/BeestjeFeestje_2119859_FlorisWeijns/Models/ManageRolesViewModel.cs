using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
