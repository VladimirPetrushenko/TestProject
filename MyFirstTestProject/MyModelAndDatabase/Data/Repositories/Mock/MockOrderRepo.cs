using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Data.Repositories.Mock;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data
{
    public class MockOrderRepo : MockRepo, IRepository<Order>
    {
        public MockOrderRepo() : base()
        {
        }

        public void CreateItem(Order order)
        {
            if (order == null) 
            { 
                throw new ArgumentException(null, nameof(order));
            }
            Orders.Add(order);
        }

        public void DeleteItem(Order order)
        {
            if (order == null) 
            { 
                throw new ArgumentException("Argument is null", nameof(order));
            }
            Orders.Remove(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return Orders;
        }

        public Task<Order> GetByID(int id)
        {
            return Task.FromResult(Orders.Where(x => x.Id == id).FirstOrDefault());
        }

        public void UpdateItem(Order order)
        {
            //nothing
        }

        public Task<bool> SaveChanges()
        {
            return Task.FromResult(true);
        }

        public Task<bool> ItemExists(int id)
        {
            return Task.FromResult(Orders.Any(x => x.Id == id));
        }

        public IEnumerable<Order> GetItemsWithName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
