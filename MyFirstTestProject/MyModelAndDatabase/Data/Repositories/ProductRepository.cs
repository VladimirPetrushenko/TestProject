using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _context.Products.Add(product);
        }

        public void DeleteItem(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Task<Product> GetByID(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public Task<bool> ItemExists(int id)
        {
            return _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateItem(Product product)
        {
            //nothing
        }
    }
}
