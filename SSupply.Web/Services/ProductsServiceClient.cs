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

            var response = client.Execute(request);
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products", Method.GET);

            var response = client.Execute(request);
        }

        public ProductViewModel GetProductById(Guid id)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/{id}", Method.GET);

            request.AddUrlSegment("id", id);

            var response = client.Execute<ProductViewModel>(request);

            return response;
        }

        public void InsertProduct(ProductDto productDto)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(productDto);

            var response = client.Execute<ProductViewModel>(request);
        }

        public List<ProductViewModel> SearchProductByName(string term)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/search/{term}", Method.GET);

            request.AddUrlSegment("term", term);

            var response = client.Execute<List<ProductDto>>(request);

            return response;
        }

        public void UpdateProduct(ProductDto productDto)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Products/{id}", Method.PUT);

            request.AddUrlSegment("id", productDto.Id);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(productDto);

            client.Execute<ProductViewModel>(request);
        }
    }
}
