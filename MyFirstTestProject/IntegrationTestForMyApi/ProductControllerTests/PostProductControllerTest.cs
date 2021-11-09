using IntegrationTestForMyApi.Extentions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class PostProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Post_BadModel_StatusCode400()
        {
            var addProductModel = fixture.CreateAddProductWithoutAlias();

            var response = await CreateProductAsync(addProductModel);

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());

            response.CheckResponse(HttpStatusCode.OK);
        }
    }
}
