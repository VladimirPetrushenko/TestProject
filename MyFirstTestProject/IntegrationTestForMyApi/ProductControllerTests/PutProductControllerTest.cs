using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class PutProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Put_ReturnsProduct_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();
            product.Name = "New name";

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));
            var returnResult = await response.Content.ReadAsAsync<Product>();

            response.CheckResponse(HttpStatusCode.OK);
            CheckReturnResult(returnResult, product);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();
            product.Type = ProductType.None;

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdateProductAsync(new UpdateProduct { Id = 0 });

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact] 
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(Routs.BadRoute, new UpdateProduct());

            response.CheckResponse(HttpStatusCode.NotFound);
        }
    }
}
