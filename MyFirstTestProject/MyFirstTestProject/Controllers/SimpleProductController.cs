using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Controllers
{
    [Authorize]
    public class SimpleProductController : Controller
    {
        readonly List<Product> products = new();

        public SimpleProductController() { }

        public SimpleProductController(List<Product> products)
        {
            this.products = products;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts());
        }

        public IActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IActionResult> GetProductAsync(int id)
        {
            return await Task.FromResult(GetProduct(id));
        }
    }
}