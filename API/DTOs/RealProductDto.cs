using System;

namespace API.DTOs
{
    public class RealProductDto
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string SerialNumber { get; set; }
        public string Condition { get; set; }

        public Decimal RentPrice { get; set; }
    }
}