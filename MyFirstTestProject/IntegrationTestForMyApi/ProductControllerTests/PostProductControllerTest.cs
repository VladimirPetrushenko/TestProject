using IntegrationTestForMyApi.Extentions;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class PostProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Post_BadModel_StatusCode400()
        {
            var addProductModel = CreateAddProductWithoutAlias();

            var response = await CreateProductAsync(addProductModel);

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();

            CheckResponse(response, HttpStatusCode.OK);

            await EndProductTest(product);
        }
    }
}
