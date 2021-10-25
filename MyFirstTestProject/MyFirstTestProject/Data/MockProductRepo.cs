using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Data
{
    public class MockProductRepo : IRepository<Product>
    {
        public List<Product> products;
        public MockProductRepo()
        {
            products = new List<Product>
            {
                new Product { Id = 0, Alias = "first product", Name = "milk", Type = ProductType.Main },
                new Product { Id = 1, Alias = "Hot chocolate", Name = "Kakao", Type = ProductType.Main },
                new Product { Id = 2, Alias = "Chocolate and nuts", Name = "Nuts", Type = ProductType.Others },
            };
            
        }
        public void CreateItem(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException(nameof(product));
            }
            products.Add(product);
        }

        public void DeleteItem(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException(nameof(product));
            }
            products.Remove(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product GetByID(int id)
        {
            return products.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateItem(Product product)
        {
            //nothing
        }
    }
}
