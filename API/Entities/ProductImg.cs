namespace API.Entities
{
    public class ProductImg
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Link { get; set; }
        
        public string PublicId { get; set; }
    }
}