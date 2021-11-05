using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly MyContext _context;

        public PersonRepository(MyContext context)
        {
            _context = context;
        }

        public void CreateItem(Person person) =>
            _context.People.Add(person);

        public void DeleteItem(Person person) => 
            _context.People.Remove(person);

        public IEnumerable<Person> GetAll() => 
            _context.People;

        public Task<Person> GetByID(int id) =>
            _context.People
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        public void UpdateItem(Person person)
        {
            //nothing
        }

        public async Task<bool> SaveChanges() =>
            await _context.SaveChangesAsync() >= 0;

        public Task<bool> ItemExists(int id) =>
            _context.People.AnyAsync(x => x.Id == id);
    }
}
