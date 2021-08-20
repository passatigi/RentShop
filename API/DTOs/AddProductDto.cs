
using System.Collections.Generic;

namespace API.DTOs
{
    public class AddProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public ICollection<AddProductFeatureDto> ProductFeatures { get; set; }

    }
}