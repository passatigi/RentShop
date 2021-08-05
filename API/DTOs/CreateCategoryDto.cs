namespace API.DTOs
{
    public class CreateCategoryDto
    {
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string ImgLink { get; set; }
    }
}