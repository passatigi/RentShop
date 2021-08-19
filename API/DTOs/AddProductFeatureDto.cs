namespace API.DTOs
{
    public class AddProductFeatureDto
    {
        public int ProductId { get; set; }
        public int FeatureId { get; set; }
        public string Value { get; set; }
    }
}