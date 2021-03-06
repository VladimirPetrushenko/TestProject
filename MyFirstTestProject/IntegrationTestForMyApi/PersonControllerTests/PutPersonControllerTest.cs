using IntegrationTestForMyApi.Extentions;
using IntegrationTestForMyApi.Extentions.Fixture;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PutPersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Put_ReturnsProduct_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();
            person.FirstName = "NewFirstName";

            response = await UpdatePersonAsync(person.CreateUpdatePersonFromPerson());
            var returnResult = await response.Content.ReadAsAsync<Person>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.CheckReturnResult(person);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            person.FirstName = "This string is more than 20 symbols"; 
            response = await UpdatePersonAsync(person.CreateUpdatePersonFromPerson());

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdatePersonAsync(new UpdatePerson { Id = 0 });

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(Routs.BadRoute, new UpdatePerson());

            response.CheckResponse(HttpStatusCode.NotFound);
        }
    }
}
