using AutoFixture;
using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IntegrationTestForMyApi.Extentions;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class ProductControllerTest : IntegrationTest
    {        
        protected async Task<HttpResponseMessage> DeleteProductAsync(DeleteProduct product) =>
            await TestClient.DeleteAsync(product, Routs.Product);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(Routs.Product, product);

        protected async Task<HttpResponseMessage> UpdateProductAsync(UpdateProduct product) =>
            await TestClient.PutAsJsonAsync(Routs.Product, product);

        protected async Task<HttpResponseMessage> GetProductAsync(int? id) =>
            await TestClient.GetAsync(Routs.Product + id);
    }
}
