using System;

namespace API.Entities
{
    public class RealProduct
    {
        public int Id { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string SerialNumber { get; set; }

        public Decimal RentPrice { get; set; }

        public DateTime DateAvaible { get; set; }
    }
}