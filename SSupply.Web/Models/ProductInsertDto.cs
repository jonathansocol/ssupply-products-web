using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SSupply.Web.Models
{
    public class ProductInsertDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
