using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly MyContext _context;

        public OrderRepository(MyContext context)
        {
            _context = context;
        }

        public void CreateItem(Order item)
        {
            _context.Add(item);
        }

        public void DeleteItem(Order item)
        {
            _context.Remove(item);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
                .Include(x => x.Person)
                .Include(x => x.Products);
        }

        public Task<Order> GetByID(int id)
        {
            return _context.Orders.Where(x => x.Id == id)
                .Include(x=>x.Person)
                .Include(x=>x.Products)
                .FirstOrDefaultAsync();
        }

        public Task<bool> ItemExists(int id)
        {
            return _context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateItem(Order item)
        {
            // Nothing
        }
    }
}
