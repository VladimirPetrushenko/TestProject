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

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnDeletedPerson_StatusCode200()
        {
            var response = await CreatePersonAsync(CreateValideAddModel());
            var person = await response.Content.ReadAsAsync<Person>();

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            CheckResponse(response, HttpStatusCode.OK);
            var returnResult = await response.Content.ReadAsAsync<Person>();
            CheckReturnResult(returnResult, person);
        }

        [Fact]
        public async Task Delete_WhenPersonIsActiveFalse_StatusCode400()
        {
            var model = CreateValideAddModel();
            model.IsActive = false;
            var response = await CreatePersonAsync(model);
            var person = await response.Content.ReadAsAsync<Person>();

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            CheckResponse(response, HttpStatusCode.BadRequest);

            person.IsActive = true;
            await UpdatePersonAsync(CreateUpdatePersonFromPerson(person));
            
            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            CheckResponse(response, HttpStatusCode.OK);
        }
    }
}
