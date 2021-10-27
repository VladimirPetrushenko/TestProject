using Microsoft.AspNetCore.Mvc;
using MyFirstTestProject.Controllers;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Tests
{
    public class SimpleProductControllerTests
    {
        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.Equal(testProducts.Count, result.Count);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = await controller.GetAllProductsAsync() as List<Product>;
            Assert.Equal(testProducts.Count, result.Count);
        }

        [Fact]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var objectResult = controller.GetProduct(4) as OkObjectResult;
            var result = objectResult.Value as Product;
            Assert.NotNull(result);
            Assert.Equal(testProducts[3].Name, result.Name);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var objectResult = await controller.GetProductAsync(4) as OkObjectResult;
            var result = objectResult.Value as Product;
            Assert.NotNull(result);
            Assert.Equal(testProducts[3].Name, result.Name);
        }

        [Fact]
        public void GetProduct_ShouldNotFindProduct()
        {
            var controller = new SimpleProductController(GetTestProducts());

            var result = controller.GetProduct(999);
            Assert.IsType<NotFoundResult>(result);
        }

        private static List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Demo1", Alias = "1" },
                new Product { Id = 2, Name = "Demo2", Alias = "3.75M" },
                new Product { Id = 3, Name = "Demo3", Alias = "16.99M" },
                new Product { Id = 4, Name = "Demo4", Alias = "11.00M" }
            };

            return testProducts;
        }

       
    }
}