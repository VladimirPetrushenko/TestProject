using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Orders;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class PostOrderControllerTest : OrderControllerTest
    {
        [Fact]
        public async Task Post_WhenPersonOrProductNotFound_StatusCode400()
        {
            var response = await CreateOrderAsync(new AddOrder { Person = 0, Products = new List<int>() { 0 } });

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_NormalModelt_StatusCode200()
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

            response = await CreateOrderAsync(order);
            response.CheckResponse(HttpStatusCode.OK);
        }
    }
}
