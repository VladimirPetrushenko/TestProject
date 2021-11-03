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
            var response = await CreateProductAsync(CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();
            product.Name = "New name";

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));
            var returnResult = await response.Content.ReadAsAsync<Product>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, product);

            await EndProductTest(product);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await CreateProductAsync(CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();
            product.Type = ProductType.None;

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));

            CheckResponse(response, HttpStatusCode.BadRequest);

            await EndProductTest(product);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdateProductAsync(new UpdateProduct { Id = 0, Alias = "Milk", Name = "Saw product", Type = ProductType.Main });

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact] 
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(baseRoute + "something", new UpdateProduct());

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
