using AutoFixture;
using FluentAssertions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IntegrationTestForMyApi.Extentions;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PersonControllerTest : IntegrationTest
    {
        protected async Task<HttpResponseMessage> DeletePersonAsync(DeletePerson person) =>
            await TestClient.DeleteAsync(person, Routs.Person);

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(Routs.Person, person);

        protected async Task<HttpResponseMessage> UpdatePersonAsync(UpdatePerson person) =>
            await TestClient.PutAsJsonAsync(Routs.Person, person);

        protected async Task<HttpResponseMessage> GetPersonAsync(int? id) =>
            await TestClient.GetAsync(Routs.Person + id);
    }
}
