using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyClient.DataAccess
{
    public interface IClientPerson
    {
        [Get("/Person/{id}")]
        Task<Person> GetPerson(int id);

        [Get("/Person/GetAll")]
        Task<List<Person>> GetPersons();

        [Post("/Person/")]
        Task<Person> CreatePerson([Body] AddPerson Person);

        [Put("/Person/")]
        Task<Person> UpdatePerson([Body] UpdatePerson Person);

        [Delete("/Person/")]
        Task<Person> DeletePersons([Body] DeletePerson Person);
    }
}
