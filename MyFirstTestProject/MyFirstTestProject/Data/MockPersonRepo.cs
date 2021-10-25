using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Data
{
    public class MockPersonRepo : IPersonRepo
    {
        List<Person> people;

        public MockPersonRepo()
        {
            people = new()
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
            people.Add(person);
        }

        public void DeletePerson(Person person)
        {
            if (person == null)
                throw new ArgumentException(nameof(person));
            people.Remove(person);
        }

        public IEnumerable<Person> GetPeople()
        {
            return people;
        }

        public Person GetPersonById(int id)
        {
            return people.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdatePerson(Person person)
        {
            var temp = people.Where(x => x.Id == person.Id).First();
            temp = person;
        }
    }
}
