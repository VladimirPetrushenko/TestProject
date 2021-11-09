using IntegrationTestForMyApi.Extentions;
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

            response = await UpdatePersonAsync(CreateUpdatePersonFromPerson(person));
            var returnResult = await response.Content.ReadAsAsync<Person>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, person);

            await EndPersonTest(person);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            person.FirstName = "This string is more than 20 symbols"; 
            response = await UpdatePersonAsync(CreateUpdatePersonFromPerson(person));

            CheckResponse(response, HttpStatusCode.BadRequest);

            await EndPersonTest(person);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdatePersonAsync(new UpdatePerson { Id = 0 });

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(baseRoute + "something", new UpdatePerson());

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
