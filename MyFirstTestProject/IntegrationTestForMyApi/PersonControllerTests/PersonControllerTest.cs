using FluentAssertions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PersonControllerTest : IntegrationTest
    {
        protected string controllerName = "person/";

        protected static void CheckReturnResult(Person returnResult, Person person)
        {
            returnResult.Id.Should().Be(person.Id);
            returnResult.FirstName.Should().Be(person.FirstName);
            returnResult.LastName.Should().Be(person.LastName);
            returnResult.IsActive.Should().Be(person.IsActive);
            returnResult.IsBlock.Should().Be(person.IsBlock);
        }

        protected async Task<HttpResponseMessage> DeletePersonAsync(DeletePerson person)
        {
            HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(person),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(baseRoute + controllerName)
            };

            return await TestClient.SendAsync(request);
        }

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) => 
            await TestClient.PostAsJsonAsync(baseRoute + controllerName, person);
        
        protected async Task<HttpResponseMessage> UpdatePersonAsync(UpdatePerson person) =>
            await TestClient.PutAsJsonAsync(baseRoute + controllerName, person);

        protected static AddPerson CreateValideAddModel() => 
            new() { FirstName = "Vladimir", LastName = "Petrusneko", Email = "someEmail@mail", IsActive = true };

        protected static UpdatePerson CreateUpdatePersonFromPerson(Person person) => 
            new() { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, IsActive = person.IsActive, IsBlock = person.IsBlock };

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code) =>
            response.StatusCode.Should().Be(code);

        protected async Task EndPersonTest(Person person) => 
            await DeletePersonAsync(new DeletePerson { Id = person.Id });
    }
}
