namespace API.Entities
{
    public class OrderProduct
    {
        //public int Id { get; set; }
        public int RealProductId { get; set; }
        public RealProduct RealProduct { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
    }
}