using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data
{
    public class MockPersonRepo : IRepository<Person>
    {
        public List<Person> People { get; set; }

        public MockPersonRepo()
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
        }

        public void CreateItem(Person person)
        {
            if (person == null) 
            { 
                throw new ArgumentException(null, nameof(person));
            }
            People.Add(person);
        }

        public void DeleteItem(Person person)
        {
            if (person == null) 
            { 
                throw new ArgumentException("Argument is null", nameof(person));
            }
            People.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return People;
        }

        public Task<Person> GetByID(int id)
        {
            return Task.FromResult(People.Where(x => x.Id == id).FirstOrDefault());
        }

        public void UpdateItem(Person person)
        {
            //nothing
        }

        public Task<bool> SaveChanges()
        {
            return Task.FromResult(true);
        }

        public Task<bool> ItemExists(int id)
        {
            return Task.FromResult(People.Any(x => x.Id == id));
        }
    }
}
