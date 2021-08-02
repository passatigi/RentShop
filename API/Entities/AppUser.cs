using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public ICollection<Address> Addresses { get; set; }


       // more collections later public ICollection<Address> Addresses { get; set; }

        
    }
}