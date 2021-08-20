namespace API.DTOs
{
    public class ProductImgDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Link { get; set; }
        public string PublicId { get; set; }
    }
}