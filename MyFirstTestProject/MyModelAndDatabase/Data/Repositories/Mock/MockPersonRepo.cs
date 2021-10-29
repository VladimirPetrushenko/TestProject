using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MyModelAndDatabase.Data.Interfaces;

namespace MyModelAndDatabase.Data
{
    public class MockPersonRepo : IRepository<Person>
    {
        public List<Person> People { get; set; }

        public MockPersonRepo()
        {
            People = new()
            {
                new Person { Id = 1, FirstName = "Vladimir", LastName = "Petrushenko" },
                new Person { Id = 2, FirstName = "Igor", LastName = "Ivanov" },
                new Person { Id = 3, FirstName = "Ivan", LastName = "Britva" },
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

        public Person GetByID(int id)
        {
            return People.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateItem(Person person)
        {
            //nothing
        }

        public bool SaveChanges()
        {
            return true;
        }

        public bool ItemExists(int id)
        {
            return People.Any(x => x.Id == id);
        }
    }
}
