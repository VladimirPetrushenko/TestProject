using MyClient.Models.Products;
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
            var response = await CreateProductAsync(new AddProduct { Alias = null, Name = "something", Type = ProductType.Others });

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_ReturnsProduct_StatusCode200()
        {
            var response = await CreateProductAsync(new AddProduct { Alias = "milk", Name = "Saw product", Type = ProductType.Others });
            var product = await response.Content.ReadAsAsync<Product>();
            CheckResponse(response, HttpStatusCode.OK);
            response = await DeleteProductAsync(new DeleteProduct { Id = product.Id });
            var returnResult = await response.Content.ReadAsAsync<Product>();
            CheckReturnResult(returnResult, product);
        }
    }
}
