using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi
{
    public class ProductControllerTest : IntegrationTest
    {
        private string controllerName = "Product/";

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + controllerName);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Product>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsProduct_ThenDeleteProduct_StatusCode200()
        {
            var product = await CreatePostProductAsync(new AddProduct { Alias = "Milk", Name = "Saw product", Type = ProductType.Main });

            var response = await TestClient.GetAsync(baseRoute + controllerName + 1);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var returnResult = await response.Content.ReadAsAsync<Product>();
            returnResult.Id.Should().Be(product.Id);
            returnResult.Alias.Should().Be(product.Alias);
            returnResult.Name.Should().Be(product.Name);
            returnResult.Type.Should().Be(product.Type);

            response = await TestClient.Delete(baseRoute + controllerName + 1);

        }

        private async Task<Product> CreatePostProductAsync(AddProduct product)
        {
            var response = await TestClient.PostAsJsonAsync(baseRoute + "/product/", product);
            return await response.Content.ReadAsAsync<Product>();
        }

        private async Task<Product> CreatePostProductAsync(DeleteProduct product)
        {
            var response = await TestClient.DeleteAsJsonAsync(baseRoute + "/product/", product);
            return await response.Content.ReadAsAsync<Product>();
        }
    }
}
