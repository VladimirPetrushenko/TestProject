using IntegrationTest.Extentions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.ProductControllerTests
{
    public class DeleteProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Delete_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await DeleteProductAsync(new DeleteProduct { Id = 0});

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnDeletedProduct_StatusCode200()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();

            response = await DeleteProductAsync(new DeleteProduct { Id = product.Id });
            var returnResult = await response.Content.ReadAsAsync<Product>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, product);
        }
    }
}
