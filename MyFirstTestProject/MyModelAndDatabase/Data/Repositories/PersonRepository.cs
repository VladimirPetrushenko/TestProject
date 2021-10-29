using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MyModelAndDatabase.Data.Interfaces;

namespace MyModelAndDatabase.Data.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly MyContext _context;

        public PersonRepository(MyContext context)
        {
            _context = context;
        }

        public void CreateItem(Person person)
        {
            if (person == null)
            {
                throw new ArgumentException(null, nameof(person));
            }
            _context.People.Add(person);
        }

        public void DeleteItem(Person person)
        {
            if (person == null)
            {
                throw new ArgumentException(null, nameof(person));
            }
            _context.People.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.People;
        }

        public Person GetByID(int id)
        {
            return _context.People.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Find(int id)
        {
            return _context.People.Any(x => x.Id == id);
        }

        public void UpdateItem(Person person)
        {
            //nothing
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool ItemExists(int id)
        {
            return _context.People.Any(x => x.Id == id);
        }
    }
}
