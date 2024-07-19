using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BeestjeFeestje_2119859_FlorisWeijns.Models
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public IList<IdentityRole> AvailableRoles { get; set; }
    }
}
