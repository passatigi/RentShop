using System.Collections.Generic;

namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Feature> Features { get; set; }
        public string Name { get; set; }
        public string ImgLink { get; set; }
    }
}