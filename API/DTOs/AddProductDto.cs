namespace API.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

    }
}