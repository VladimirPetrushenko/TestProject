using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.ProductControllerTests
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
            var response = await CreateProductAsync(new AddProduct { Alias = "milk", Name = "Saw product", Type = ProductType.Others });
            var product = await response.Content.ReadAsAsync<Product>();

            response = await DeleteProductAsync(new DeleteProduct { Id = product.Id });
            CheckResponse(response, HttpStatusCode.OK);
            var returnResult = await response.Content.ReadAsAsync<Product>();
            CheckReturnResult(returnResult, product);
        }
    }
}
