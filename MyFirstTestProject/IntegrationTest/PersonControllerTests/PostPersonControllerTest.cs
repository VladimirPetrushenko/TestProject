using IntegrationTest.Extentions;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.PersonControllerTests
{
    public class PostPersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Post_BadModel_StatusCode400()
        {
            var model = CreateAddModelWhithoutLastName();

            var response = await CreatePersonAsync(model);

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
        {
            var response = await CreatePersonAsync(fixture.CreateValideAddPerson());
            var person = await response.Content.ReadAsAsync<Person>();

            CheckResponse(response, HttpStatusCode.OK);

            await EndPersonTest(person);
        }
    }
}
