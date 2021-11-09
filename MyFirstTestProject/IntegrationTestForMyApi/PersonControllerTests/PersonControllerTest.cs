using AutoFixture;
using FluentAssertions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PersonControllerTest : IntegrationTest
    {
        protected static void CheckReturnResult(Person returnResult, Person person)
        {
            returnResult.Id.Should().Be(person.Id);
            returnResult.FirstName.Should().Be(person.FirstName);
            returnResult.LastName.Should().Be(person.LastName);
            returnResult.IsActive.Should().Be(person.IsActive);
            returnResult.IsBlock.Should().Be(person.IsBlock);
        }

        protected async Task<HttpResponseMessage> DeletePersonAsync(DeletePerson person) =>
            await DeleteAsync(person, Routs.Person);

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(Routs.Person, person);

        protected async Task<HttpResponseMessage> UpdatePersonAsync(UpdatePerson person) =>
            await TestClient.PutAsJsonAsync(Routs.Person, person);

        protected AddPerson CreateAddModelWhithoutLastName() =>
            fixture.Build<AddPerson>()
                .With(p => p.FirstName, "Vladimir")
                .Without(p => p.LastName)
                .With(p => p.IsActive, true)
                .Create();

        protected AddPerson CreateAddModelWhithoutIsActive() =>
            fixture.Build<AddPerson>()
                .With(p => p.FirstName, "Vladimir")
                .With(p => p.LastName, "Petrushenko")
                .With(p => p.IsActive, false)
                .Create();

        protected static UpdatePerson CreateUpdatePersonFromPerson(Person person) => 
            new() { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, IsActive = person.IsActive, IsBlock = person.IsBlock };
    }
}
