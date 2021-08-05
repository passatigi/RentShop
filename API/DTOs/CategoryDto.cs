using System.Collections.Generic;

namespace API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public ICollection<CategoryDto> ChildCategories { get; set; }
        public string Name { get; set; }
        public string ImgLink { get; set; }
    }
}