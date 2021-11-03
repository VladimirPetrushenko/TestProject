using FluentAssertions;
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
            var returnResult = await response.Content.ReadAsAsync<Person>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, person);

            await EndPersonTest(person);
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName);
            var returnResult = await response.Content.ReadAsAsync<List<Product>>();

            CheckResponse(response, HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
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
            person.IsBlock = true;
            await UpdatePersonAsync(CreateUpdatePersonFromPerson(person));
            
            response = await TestClient.GetAsync(baseRoute + controllerName + person.Id);

            CheckResponse(response, HttpStatusCode.BadRequest);

            await EndPersonTest(person);
        }
    }
}
