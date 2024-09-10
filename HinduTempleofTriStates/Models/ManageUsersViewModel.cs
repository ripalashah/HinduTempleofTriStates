using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HinduTempleofTriStates.Models
{
    public class ManageUsersViewModel
    {
        public required List<UserViewModel> Users { get; set; }
        public required List<IdentityRole> Roles { get; set; }
    }

    public class UserViewModel
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required List<string> UserRoles { get; set; }
    }
}
