using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Dtos.Orders;
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
    public class PutOrderControllerTest : OrderControllerTest
    {
        [Fact]
        public async Task Put_ReturnsOrder_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await Initialize();
            var order = await response.Content.ReadAsAsync<OrderReadDto>();
            response = await GetPersonAsync(null);
            var person = await response.Content.ReadAsAsync<List<Person>>();

            var updateOrder = new UpdateOrder
            {
                Id = order.Id,
                Person = person.Last().Id,
                Products = order.Products.Select(p => p.Id).ToList()
            };

            response = await UpdateOrderAsync(updateOrder);
            var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

            response.CheckResponse(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await Initialize();
            var order = await response.Content.ReadAsAsync<OrderReadDto>();

            var updateOrder = new UpdateOrder
            {
                Id = order.Id,
                Person = 0,
                Products = order.Products.Select(p => p.Id).ToList()
            };

            response = await UpdateOrderAsync(updateOrder);

            response.CheckResponse(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdateOrderAsync(new UpdateOrder { Id = 0 });

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await UpdateOrderAsync(new UpdateOrder());

            response.CheckResponse(HttpStatusCode.NotFound);
        }
    }
}
