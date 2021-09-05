using System.Collections.Generic;

namespace API.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public int? AppUserId { get; set; }

        public string Country { get; set; }
        public string City  { get; set; }
        public string HouseAddress { get; set; }
        public string PostalCode { get; set; }

        public ICollection<Order> ShippedOrders { get; set; }

        public ICollection<Order> ReturnOrders { get; set; }
    }
}