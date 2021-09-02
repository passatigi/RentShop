namespace API.DTOs
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}