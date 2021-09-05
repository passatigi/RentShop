using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public DateTime RequiredReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; }

        public string Comments { get; set; }

        public int DeliverymanId { get; set; }
        public AppUser Deliveryman { get; set; }

        public int CustomerId { get; set; }
        public AppUser Customer { get; set; }

        public int ShippedAddressId { get; set; }
        public Address ShippedAddress { get; set; }

        public int ReturnAddressId { get; set; }
        public Address ReturnAddress { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}