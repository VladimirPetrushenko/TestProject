using FluentAssertions;
using IntegrationTest.Extentions;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.ProductControllerTests
{
    public class GetProductControllerTest : ProductControllerTest
    {
        [Fact]
        public async Task Get_ReturnsProduct_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();

            response = await TestClient.GetAsync(baseRoute + controllerName + product.Id);
            var returnResult = await response.Content.ReadAsAsync<Product>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, product);

            await EndProductTest(product);
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName);
            var returnResult = await response.Content.ReadAsAsync<List<Product>>();

            CheckResponse(response, HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName + 0);

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
