using RestSharp;
using SSupply.Web.Interfaces;
using SSupply.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Services
{
    public class ProductsServiceClient : IProductsServiceClient
    {
        private readonly string _baseUrl;

        public ProductsServiceClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void DeleteProduct(Guid id)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/{id}", Method.DELETE);

            request.AddUrlSegment("id", id);

            client.Execute(request);
        }

        public List<ProductDto> GetAllProducts()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products", Method.GET);

            var response = client.Execute<List<ProductDto>>(request);

            return response.Data;
        }

        public ProductDto GetProductById(Guid id)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/{id}", Method.GET);

            request.AddUrlSegment("id", id);

            var response = client.Execute<ProductDto>(request);

            return response.Data;
        }

        public void InsertProduct(ProductViewModel productDto)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(productDto);

            client.Execute(request);
        }

        public List<ProductDto> SearchProductByName(string term)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/search/{term}", Method.GET);

            request.AddUrlSegment("term", term);

            var response = client.Execute<List<ProductDto>>(request);

            return response.Data;
        }

        public void UpdateProduct(ProductDto product)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/{id}", Method.PUT);

            request.AddUrlSegment("id", product.Id);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(product);

            client.Execute(request);
        }
    }
}
