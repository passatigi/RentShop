namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public string Name { get; set; }

        public string ImgLink { get; set; }
    }
}