using FluentAssertions;
using IntegrationTestForMyApi.Extentions;
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

            response = await GetOrderAsync(order.Id);
            var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.CheckReturnResult(order);
        }

        [Fact]
        public async Task Get_WithoutAnyPost_ReturnEmptyResponce_StatusCode200()
        {
            var response = await GetOrderAsync(null);
            var returnResult = await response.Content.ReadAsAsync<List<OrderReadDto>>();

            response.CheckResponse(HttpStatusCode.OK);
            returnResult.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await GetOrderAsync(0);

            response.CheckResponse(HttpStatusCode.NotFound);
        }
    }
}
