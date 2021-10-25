using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Data
{
    public class MockPersonRepo : IPersonRepo
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

        public void CreatePerson(Person person)
        {
            if (person == null)
                throw new ArgumentException(nameof(person));
            People.Add(person);
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentException("Argument is null", nameof(person));
            People.Remove(person);
        }

        public IEnumerable<Person> GetPeople()
        {
            return People;
        }

        public Person GetPersonById(int id)
        {
            return People.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdatePerson(Person person)
        {
            //nothing
        }
    }
}
