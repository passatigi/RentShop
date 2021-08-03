using System.Collections.Generic;

namespace API.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductImg> ProductImgs { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        public ICollection<RealProduct> RealProducts { get; set; }
    }
}