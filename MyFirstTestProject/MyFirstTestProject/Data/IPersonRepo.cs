using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Data
{
    public interface IPersonRepo
    {
        IEnumerable<Person> GetPeople();
        Person GetPersonById(int id);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
