namespace API.Entities
{
    public class ProductFeature
    {
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Value { get; set; }
    }
}