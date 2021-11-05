using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data
{
    public class MockOrderRepo : IRepository<Order>
    {
        public List<Order> Orders { get; set; }
        public List<Person> People { get; set; }
        public List<Product> Products { get; set; }

        public MockOrderRepo()
        {
            People = new()
            {
                new Person { Id = 1, FirstName = "Vladimir", LastName = "Petrushenko", IsActive = true },
                new Person { Id = 2, FirstName = "Igor", LastName = "Ivanov", Email = "someemail@mail.ru", IsActive = true },
                new Person { Id = 3, FirstName = "Ivan", LastName = "Britva", IsActive = true, IsBlock = true },
            };

            Products = new List<Product>
            {
                new Product { Id = 1, Alias = "first product", Name = "milk", Type = ProductType.Main },
                new Product { Id = 2, Alias = "Hot chocolate", Name = "Kakao", Type = ProductType.Main },
                new Product { Id = 3, Alias = "Chocolate and nuts", Name = "Nuts", Type = ProductType.Others },
            };

            Orders = new()
            {
                new Order { Id = 1, Person = People[0], Products = Products.Take(2).ToList() },
                new Order { Id = 2, Person = People[1], Products = Products.Skip(1).ToList() },
                new Order { Id = 3, Person = People[2], Products = Products.Skip(1).Take(1).ToList() },
                new Order { Id = 4, Person = People[0], Products = Products.SkipLast(2).ToList() },
            };
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
    }
}
