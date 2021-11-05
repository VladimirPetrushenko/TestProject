using FluentAssertions;
using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using MyModelAndDatabase.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class OrderControllerTest : IntegrationTest
    {
        protected string controllerName = "person/";
        private int RowCount = 100;

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

        protected async Task<HttpResponseMessage> CreatePersonAsync(AddPerson person) =>
            await TestClient.PostAsJsonAsync(baseRoute + controllerName, person);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(baseRoute + controllerName, product);

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
            await DeleteItems<Order, DeleteOrder>("order/");
            await DeleteItems<Person, DeletePerson>("person/");
            await DeleteItems<Product, DeleteProduct>("product/");
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
