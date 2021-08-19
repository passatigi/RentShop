using System.Collections.Generic;

namespace API.DTOs
{
    public class DetailedProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<string> ProductImgsLinks { get; set; }
        public ICollection<ProductFeatureDto> ProductFeatures { get; set; }
        public ICollection<RealProductDto> RealProducts { get; set; }
    }
}