using IntegrationTestForMyApi.Extentions;
using IntegrationTestForMyApi.Extentions.Fixture;
using MyClient.Models.Orders;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class OrderControllerTest : IntegrationTest
    {
        protected int RowCount = 100;

        protected async Task<HttpResponseMessage> GetPersonAsync(int? id) =>
            await TestClient.GetAsync(Routs.Person + id);

        protected async Task<HttpResponseMessage> GetProductAsync(int? id) =>
            await TestClient.GetAsync(Routs.Product + id);

        protected async Task<HttpResponseMessage> GetOrderAsync(int? id) =>
            await TestClient.GetAsync(Routs.Order + id);

        protected async Task<HttpResponseMessage> CreateOrderAsync(AddOrder order) =>
            await TestClient.PostAsJsonAsync(Routs.Order, order);

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(Routs.Person, person);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(Routs.Product, product);

        protected async Task<HttpResponseMessage> DeleteOrderAsync(DeleteOrder order) =>
            await TestClient.DeleteAsync(order, Routs.Order);

        protected async Task<HttpResponseMessage> UpdateOrderAsync(UpdateOrder order) =>
            await TestClient.PutAsJsonAsync(Routs.Order, order);

        protected async Task<HttpResponseMessage> Initialize()
        {
            await InitializeDatabases();

            var response = await GetPersonAsync(null);
            var person = (await response.Content.ReadAsAsync<List<Person>>()).First();
            response = await GetProductAsync(null);
            var product = await response.Content.ReadAsAsync<List<Product>>();

            var order = new AddOrder
            {
                Person = person.Id,
                Products = product.Take(5).Select(p => p.Id).ToList(),
            };

            return await CreateOrderAsync(order);
        }

        protected async Task InitializeDatabases()
        {
            for(int i = 0; i < RowCount; i++)
            {
                var person = fixture.CreateValideAddPerson();
                var product = fixture.CreateValideAddProduct();

                await CreatePersonAsync(person);
                await CreateProductAsync(product);
            }
        }
    }
}
