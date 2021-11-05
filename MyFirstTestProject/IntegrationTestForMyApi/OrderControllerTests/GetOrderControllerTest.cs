using FluentAssertions;
using MyClient.Models.Dtos.Orders;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class GetOrderControllerTest : OrderControllerTest
    {
        [Fact]
        public async Task Get_ReturnsOrder_WhenPostExistInDataBase_StatusCode200()
        {
            var response = await Initialize();
            var order = await response.Content.ReadAsAsync<OrderReadDto>();


            response = await TestClient.GetAsync(baseRoute + orderController + order.Id);
            var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, order);

            await EndOrderTest();
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await TestClient.GetAsync(baseRoute + orderController);
            var returnResult = await response.Content.ReadAsAsync<List<OrderReadDto>>();

            CheckResponse(response, HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await TestClient.GetAsync(baseRoute + orderController + 0);

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
