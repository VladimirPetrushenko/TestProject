using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class DeletePersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Delete_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await DeletePersonAsync(new DeletePerson{ Id = 0});

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnDeletedPerson_StatusCode200()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            var returnResult = await response.Content.ReadAsAsync<Person>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.CheckReturnResult(person);
        }

        [Fact]
        public async Task Delete_WhenPersonIsActiveFalse_StatusCode400()
        {
            var model = fixture.CreateAddModelWhithoutIsActive();
            var response = await CreatePersonAsync(model);
            var person = await response.Content.ReadAsAsync<Person>();

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            response.CheckResponse(HttpStatusCode.BadRequest);
        }
    }
}
