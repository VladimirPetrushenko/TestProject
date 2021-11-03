using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class ProductControllerTest : IntegrationTest
    {
        protected string controllerName = "Product/";

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code)
        {
            response.StatusCode.Should().Be(code);
        }

        protected static void CheckReturnResult(Product returnResult, Product product)
        {
            returnResult.Id.Should().Be(product.Id);
            returnResult.Alias.Should().Be(product.Alias);
            returnResult.Name.Should().Be(product.Name);
            returnResult.Type.Should().Be(product.Type);
        }

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product)
        {
            var response = await TestClient.PostAsJsonAsync(baseRoute + controllerName, product);
            return response;
        }

        protected async Task<HttpResponseMessage> DeleteProductAsync(DeleteProduct product)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(product),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(baseRoute + controllerName)
            };
            var response = await TestClient.SendAsync(request);
            return response;
        }

        protected async Task<HttpResponseMessage> UpdateProductAsync(UpdateProduct product)
        {
            var response = await TestClient.PutAsJsonAsync(baseRoute + controllerName, product);
            return response;
        }
    }
}
