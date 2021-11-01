using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data
{
    public class MockProductRepo : IRepository<Product>
    {
        public List<Product> Products { get; set; }
        public MockProductRepo()
        {
            Products = new List<Product>
            {
                new Product { Id = 1, Alias = "first product", Name = "milk", Type = ProductType.Main },
                new Product { Id = 2, Alias = "Hot chocolate", Name = "Kakao", Type = ProductType.Main },
                new Product { Id = 3, Alias = "Chocolate and nuts", Name = "Nuts", Type = ProductType.Others },
            };
            
        }
        public void CreateItem(Product product)
        {
            if (product == null) 
            {
                throw new ArgumentException(null, nameof(product));
            }
            Products.Add(product);
        }

        public void DeleteItem(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException(null, nameof(product));
            }
            Products.Remove(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return Products;
        }

        public Task<Product> GetByID(int id)
        {
            return Task.FromResult(Products.Where(x => x.Id == id).FirstOrDefault());
        }

        public Task<bool> ItemExists(int id)
        {
            return Task.FromResult(Products.Any(x => x.Id == id));
        }

        public Task<bool> SaveChanges()
        {
            return Task.FromResult(true);
        }

        public void UpdateItem(Product product)
        {
            //nothing
        }
    }
}
