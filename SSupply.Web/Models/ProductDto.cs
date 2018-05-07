using System;

namespace SSupply.Web.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public decimal Price { get; set; }
    }
}