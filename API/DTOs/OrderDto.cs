using System;
using System.Collections.Generic;

using API.Entities;


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
        public string ShippedAdress { get; set; }
        public string ReturnAdress { get; set; }

        public ICollection<RealProductDto> RealProducts { get; set; }
        
//=======

        public ICollection<RealProductDto> OrderProducts { get; set; }

        public int CustomeId { get; set; }

        //public AppUser Customer { get; set; }

        public int DeliverymanId { get; set; }

        //public AppUser Deliveryman { get; set; }

        public int AdressId { get; set; }

    }
}