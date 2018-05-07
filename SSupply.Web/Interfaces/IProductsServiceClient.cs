using SSupply.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Interfaces
{
    public interface IProductsServiceClient
    {
        IEnumerable<ProductViewModel> GetAllProducts();
        IEnumerable<ProductViewModel> SearchProductByName(string term);
        ProductViewModel GetProductById(Guid id);
        void InsertProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void DeleteProduct(Guid id);
    }
}
