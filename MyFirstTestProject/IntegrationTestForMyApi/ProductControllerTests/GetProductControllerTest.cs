using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class GetProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Get_ReturnsProduct_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await CreateProductAsync(new AddProduct { Alias = "Milk", Name = "Saw product", Type = ProductType.Main });
            var product = await response.Content.ReadAsAsync<Product>();

            response = await TestClient.GetAsync(baseRoute + controllerName + product.Id);
            CheckResponse(response, HttpStatusCode.OK);
            var returnResult = await response.Content.ReadAsAsync<Product>();
            CheckReturnResult(returnResult, product);

            await DeleteProductAsync(new DeleteProduct { Id = product.Id });
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName);

            CheckResponse(response, HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Product>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName + 0);

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
