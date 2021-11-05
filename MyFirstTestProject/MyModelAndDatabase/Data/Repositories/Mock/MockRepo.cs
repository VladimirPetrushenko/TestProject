using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyModelAndDatabase.Data.Repositories.Mock
{
    public class MockRepo
    {
        public List<Order> Orders { get; set; }
        public List<Person> People { get; set; }
        public List<Product> Products { get; set; }

        public MockRepo()
        {
            People = new()
            {
                new Person { Id = 1, FirstName = "Vladimir", LastName = "Petrushenko", IsActive = true },
                new Person { Id = 2, FirstName = "Igor", LastName = "Ivanov", Email = "someemail@mail.ru", IsActive = true },
                new Person { Id = 3, FirstName = "Ivan", LastName = "Britva", IsActive = true, IsBlock = true },
                new Person { Id = 4, FirstName = "Servei", LastName = "Britva", IsActive = false, IsBlock = true },
                new Person { Id = 5, FirstName = "Marina", LastName = "Britva", IsActive = true, IsBlock = false },
                new Person { Id = 6, FirstName = "Hanna", LastName = "Britva", IsActive = true, IsBlock = false },
                new Person { Id = 7, FirstName = "Anton", LastName = "Britva", IsActive = true, IsBlock = false },
                new Person { Id = 8, FirstName = "Boris", LastName = "Britva", IsActive = true, IsBlock = false },
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
    }
}
