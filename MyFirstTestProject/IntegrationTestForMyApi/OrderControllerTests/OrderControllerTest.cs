using FluentAssertions;
using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Dtos.Orders;
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

        protected static void CheckReturnResult(OrderReadDto returnResult, OrderReadDto order)
        {
            returnResult.Person.FirstName.Should().Be(order.Person.FirstName);
            returnResult.Person.LastName.Should().Be(order.Person.LastName);
            returnResult.Person.IsActive.Should().Be(order.Person.IsActive);
            returnResult.Person.IsBlock.Should().Be(order.Person.IsBlock);
            returnResult.Products.Count.Should().Be(order.Products.Count);
            for (int i = 0; i < returnResult.Products.Count; i++)
            {
                returnResult.Products[i].Alias.Should().Be(order.Products[i].Alias);
                returnResult.Products[i].Name.Should().Be(order.Products[i].Name);
                returnResult.Products[i].Type.Should().Be(order.Products[i].Type);
                returnResult.Products[i].Price.Should().Be(order.Products[i].Price);
            }
        }

        protected async Task<HttpResponseMessage> CreateOrderAsync(AddOrder order) =>
            await TestClient.PostAsJsonAsync(Routs.Order, order);

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(Routs.Person, person);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(Routs.Product, product);

        protected async Task<HttpResponseMessage> DeleteOrderAsync(DeleteOrder order) =>
            await DeleteAsync(order, Routs.Order);

        protected async Task<HttpResponseMessage> Initialize()
        {
            await InitializeDatabases();

            var response = await TestClient.GetAsync(Routs.Person);
            var person = (await response.Content.ReadAsAsync<List<Person>>()).First();
            response = await TestClient.GetAsync(Routs.Product); ;
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
