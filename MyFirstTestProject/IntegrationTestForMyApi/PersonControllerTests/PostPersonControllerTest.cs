using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.PersonControllerTests
{
    public class PostPersonControllerTest : PersonControllerTest
    {
        [Fact]
        public async Task Post_BadModel_StatusCode400()
        {
            var model = CreateValideAddModel();
            model.LastName = null;
            var response = await CreatePersonAsync(model);

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_ReturnsPerson_StatusCode200()
        {
            var response = await CreatePersonAsync(CreateValideAddModel());
            var person = await response.Content.ReadAsAsync<Person>();
            CheckResponse(response, HttpStatusCode.OK);

            response = await DeletePersonAsync(new DeletePerson { Id = person.Id });
            var returnResult = await response.Content.ReadAsAsync<Person>();
            CheckReturnResult(returnResult, person);
        }
    }
}
