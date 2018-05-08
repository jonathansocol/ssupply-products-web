using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using SSupply.Web.Interfaces;
using SSupply.Web.Models;

namespace SSupply.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IProductsServiceClient _productsServiceClient;
        private readonly IExcelExportService _excelExportService;

        public ProductsController(
            IImageStorageService imageStorageService,
            IProductsServiceClient productsServiceClient,
            IExcelExportService excelExportService)
        {
            _imageStorageService = imageStorageService;
            _productsServiceClient = productsServiceClient;
            _excelExportService = excelExportService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var products = _productsServiceClient.GetAllProducts();

            return View(products);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var product = _productsServiceClient.GetProductById(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Search(string term)
        {
            var products = _productsServiceClient.SearchProductByName(term);

            return View("SearchResults", products);
        }

        [HttpGet]
        public IActionResult SearchResults(List<ProductDto> products)
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductInsertDto product)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = new ErrorMessageViewModel("There was an error", "The model is invalid");

                return RedirectToAction("Error", errorMessage);
            }

            var imageUrl = await _imageStorageService.UploadFile(product.Photo);

            var productModel = new ProductViewModel
            {
                Name = product.Name,
                Photo = imageUrl,
                Price = product.Price
            };

            _productsServiceClient.InsertProduct(productModel);

            ViewData["Message"] = "New product created";

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var originalProduct = _productsServiceClient.GetProductById(id);

            var product = new ProductEditDto
            {
                Id = originalProduct.Id,
                Name = originalProduct.Name,
                Price = originalProduct.Price
            };

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductEditDto product)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = new ErrorMessageViewModel("There was an error", "The model is invalid");

                return RedirectToAction("Error", errorMessage);
            }

            string imageUrl = string.Empty;
            var originalProduct = _productsServiceClient.GetProductById(product.Id);

            if (product.Photo != null)
            {
                imageUrl = await _imageStorageService.UploadFile(product.Photo);
                await _imageStorageService.DeleteFile(originalProduct.Photo);
            }
            else
            {
                imageUrl = originalProduct.Photo;
            }

            var productModel = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Photo = imageUrl,
                Price = product.Price
            };

            _productsServiceClient.UpdateProduct(productModel);

            ViewData["Message"] = "The product was updated";

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var product = _productsServiceClient.GetProductById(id);

            if (product == null)
            {
                return RedirectToAction("Error", new ErrorMessageViewModel("Error", "The product was not found."));
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Remove(ProductDto product)
        {
            _imageStorageService.DeleteFile(product.Photo);
            _productsServiceClient.DeleteProduct(product.Id);

            ViewData["Message"] = $"The product '{product.Name}' has been deleted.";

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult Error(ErrorMessageViewModel errorMessage)
        {
            return View(errorMessage);
        }

        [HttpGet]
        public IActionResult ExportProducts()
        {
            var products = _productsServiceClient.GetAllProducts();
            var titles = new List<string[]> { new string[] { "Id", "Name", "PhotoUrl", "Price", "Last Modified" } };
            var excelFile = _excelExportService.ExportToExcel(products, "Products", titles);
            var fileName = $"Suitsupply Products - { DateTime.UtcNow.ToString("s") }.xlsx";

            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
