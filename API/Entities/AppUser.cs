using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }


        public ICollection<Address> Addresses { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Order> DeliverymanOrders { get; set; }


        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<DeliverymanSchedule> DeliverymanShedules { get; set; }
        public ICollection<DeliverySchedule> DeliverySchedules { get; set; }

       // more collections later public ICollection<Address> Addresses { get; set; }
    }
}