using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Data.Repositories.Mock;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data
{
    public class MockPersonRepo : MockRepo, IRepository<Person>
    {
        public MockPersonRepo() : base()
        {
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

        public IEnumerable<Person> GetItemsWithName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
