using FluentAssertions;
using IntegrationTestForMyApi.Extentions;
using IntegrationTestForMyApi.Extentions.Fixture;
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
            var response = await CreateProductAsync(fixture.CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();

            response = await GetProductAsync(product.Id);
            var returnResult = await response.Content.ReadAsAsync<Product>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.CheckReturnResult(product);
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await GetProductAsync(null);
            var returnResult = await response.Content.ReadAsAsync<List<Product>>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await GetProductAsync(0);

            response.CheckResponse(HttpStatusCode.NotFound);
        }
    }
}
