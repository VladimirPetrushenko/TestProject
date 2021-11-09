using MyClient.Models.Orders;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.OrderControllerTests
{
    public class PostOrderControllerTest : OrderControllerTest
    {
        [Fact]
        public async Task Post_WhenPersonOrProductNotFound_StatusCode400()
        {
            var response = await CreateOrderAsync(new AddOrder { Person = 0, Products = new List<int>() { 0 } });

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
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

            response = await CreateOrderAsync(order);
            CheckResponse(response, HttpStatusCode.OK);

            await EndOrderTest();
        }
    }
}
