using IntegrationTestForMyApi.Extentions;
using IntegrationTestForMyApi.Extentions.Fixture;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PostPersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Post_BadModel_StatusCode400()
        {
            var model = fixture.CreateAddModelWhithoutLastName();

            var response = await CreatePersonAsync(model);

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            response.CheckResponse(HttpStatusCode.OK);
        }
    }
}
