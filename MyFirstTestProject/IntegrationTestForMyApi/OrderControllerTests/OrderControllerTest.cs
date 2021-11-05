using FluentAssertions;
using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using MyModelAndDatabase.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class OrderControllerTest : IntegrationTest
    {
        protected string orderController = "order/";
        protected string productController = "product/";
        protected string personController = "person/";
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
            await TestClient.PostAsJsonAsync(baseRoute + orderController, order);

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(baseRoute + personController, person);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(baseRoute + productController, product);

        protected async Task<HttpResponseMessage> DeleteOrderAsync(DeleteOrder order) =>
            await DeleteAsync(order, orderController);

        protected async Task<HttpResponseMessage> Initialize()
        {
            await InitializeDatabases();

            var response = await TestClient.GetAsync(baseRoute + personController);
            var person = (await response.Content.ReadAsAsync<List<Person>>()).First();
            response = await TestClient.GetAsync(baseRoute + productController); ;
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
                var person = CreateValideAddPerson();
                var product = CreateValideAddProduct();

                await CreatePersonAsync(person);
                await CreateProductAsync(product);
            }
        }

        protected async Task EndOrderTest()
        {
            await DeleteItems<Order, DeleteOrder>(orderController);
            await DeleteItems<Person, DeletePerson>(personController);
            await DeleteItems<Product, DeleteProduct>(productController);
        }

        private async Task DeleteItems<T, Y>(string controller)
            where T : IId
            where Y : IId, new()
        {
            var response = await TestClient.GetAsync(baseRoute + controller);
            var returnResult = await response.Content.ReadAsAsync<List<T>>();

            foreach (var item in returnResult) { 
                await DeleteAsync(new Y() { Id = item.Id }, controller);
            }
        }
    }
}
