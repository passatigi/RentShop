namespace API.Entities
{
    public class OrderProduct
    {
        public int RealProductId { get; set; }
        public RealProduct RealProduct { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}