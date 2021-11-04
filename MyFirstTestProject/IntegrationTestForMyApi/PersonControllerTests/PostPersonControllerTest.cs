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
            var model = CreateAddModelWhithoutLastName();

            var response = await CreatePersonAsync(model);

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_ReturnsPerson_StatusCode200()
        {
            var response = await CreatePersonAsync(CreateValideAddModel());
            var person = await response.Content.ReadAsAsync<Person>();

            CheckResponse(response, HttpStatusCode.OK);

            await EndPersonTest(person);
        }
    }
}
