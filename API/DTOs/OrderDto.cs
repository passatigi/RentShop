using System;
using System.Collections.Generic;


namespace API.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime RequiredReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public UserDto Deliveryman { get; set; }
        public UserDto Customer { get; set; }
        public AddressDto ShippedAddress { get; set; }
        public AddressDto ReturnAddress { get; set; }
        public ICollection<RealProductDto> OrderProducts { get; set; }
        public int ReturnAddressId { get; set; }
        public int ShippedAddressId { get; set; }
        public int CustomerId { get; set; }
        public int DeliverymanId { get; set; }
    }
}