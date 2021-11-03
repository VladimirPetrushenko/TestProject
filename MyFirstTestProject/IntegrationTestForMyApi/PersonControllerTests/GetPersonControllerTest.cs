using FluentAssertions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class GetPersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Get_ReturnsPerson_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await CreatePersonAsync(CreateValideAddModel());
            var person = await response.Content.ReadAsAsync<Person>();

            response = await TestClient.GetAsync(baseRoute + controllerName + person.Id);
            CheckResponse(response, HttpStatusCode.OK);
            var returnResult = await response.Content.ReadAsAsync<Person>();
            CheckReturnResult(returnResult, person);

            await DeletePersonAsync(new DeletePerson { Id = person.Id });
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName);

            CheckResponse(response, HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Product>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName + 0);

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_WhenPersonExistInDataBaseAndPersonIsBlock_StatusCode400()
        {
            var response = await CreatePersonAsync(CreateValideAddModel());
            var person = await response.Content.ReadAsAsync<Person>();

            await UpdatePersonAsync(new UpdatePerson { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, IsActive = person.IsActive, IsBlock = true });
            response = await TestClient.GetAsync(baseRoute + controllerName + person.Id);
            CheckResponse(response, HttpStatusCode.BadRequest);

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            CheckResponse(response, HttpStatusCode.OK);
        }
    }
}
