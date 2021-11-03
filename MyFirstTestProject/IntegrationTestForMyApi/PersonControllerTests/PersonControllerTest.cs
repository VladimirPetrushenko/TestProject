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

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code)
        {
            response.StatusCode.Should().Be(code);
        }

        protected static void CheckReturnResult(Person returnResult, Person person)
        {
            returnResult.Id.Should().Be(person.Id);
            returnResult.FirstName.Should().Be(person.FirstName);
            returnResult.LastName.Should().Be(person.LastName);
            returnResult.IsActive.Should().Be(person.IsActive);
            returnResult.IsBlock.Should().Be(person.IsBlock);
        }

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person)
        {
            var response = await TestClient.PostAsJsonAsync(baseRoute + controllerName, person);
            return response;
        }

        protected async Task<HttpResponseMessage> DeletePersonAsync(DeletePerson person)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(person),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(baseRoute + controllerName)
            };
            var response = await TestClient.SendAsync(request);
            return response;
        }

        protected async Task<HttpResponseMessage> UpdatePersonAsync(UpdatePerson person)
        {
            var response = await TestClient.PutAsJsonAsync(baseRoute + controllerName, person);
            return response;
        }
    }
}
