using FluentAssertions;
using IntegrationTestForMyApi.Extentions;
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
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            response = await GetPersonAsync(person.Id);
            var returnResult = await response.Content.ReadAsAsync<Person>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.CheckReturnResult(person);
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await GetPersonAsync(null);
            var returnResult = await response.Content.ReadAsAsync<List<Product>>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await GetPersonAsync(0);

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_WhenPersonExistInDataBaseAndPersonIsBlock_StatusCode400()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();
            person.IsBlock = true;
            await UpdatePersonAsync(person.CreateUpdatePersonFromPerson());
            
            response = await GetPersonAsync(person.Id);

            response.CheckResponse(HttpStatusCode.BadRequest);
        }
    }
}
