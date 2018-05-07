using SSupply.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Interfaces
{
    public interface IProductsServiceClient
    {
        List<ProductDto> GetAllProducts();
        List<ProductDto> SearchProductByName(string term);
        ProductDto GetProductById(Guid id);
        void InsertProduct(ProductViewModel productDto);
        void UpdateProduct(ProductDto product);
        void DeleteProduct(Guid id);
    }
}
