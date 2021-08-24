using System;

namespace API.Entities
{
    public class DeliverymanSchedule
    {
        public int Id { get; set; }
        public int DeliverymanId { get; set; }
        public AppUser Deliveryman { get; set; }
        public DateTime StartDelivery { get; set; }
        public DateTime EndDelivery { get; set; }
    }
}