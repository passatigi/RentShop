namespace API.DTOs
{
    public class AddFeatureDto
    {
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string GroupName { get; set; }
        public int CategoryId { get; set; }
    }
}