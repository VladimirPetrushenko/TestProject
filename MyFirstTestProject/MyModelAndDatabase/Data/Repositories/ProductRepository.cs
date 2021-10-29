using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MyModelAndDatabase.Data.Interfaces;

namespace MyModelAndDatabase.Data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly MyContext _context;

        public ProductRepository(MyContext context)
        {
            _context = context;
        }

        public void CreateItem(Product product)
        {
            if(product == null)
            {
                throw new ArgumentException(null, nameof(product));
            }

            _context.Products.Add(product);
        }

        public void DeleteItem(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException(null, nameof(product));
            }

            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetByID(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool ItemExists(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateItem(Product product)
        {
            //nothing
        }
    }
}
