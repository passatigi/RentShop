namespace API.Entities
{
    public class DeliverySchedule
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public AppUser Deliveryman { get; set; }
        public bool isShipping { get; set; }
    }
}