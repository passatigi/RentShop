using System;

namespace API.Entities
{
    public class DeliverymanSchedule
    {
        public int Id { get; set; }
        public AppUser DeliveryMan { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
    }
}