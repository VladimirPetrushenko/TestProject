using IntegrationTestForMyApi.Extentions;
using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestForMyApi.OrderControllerTests
{
    public class DeleteOrderControllerTest : OrderControllerTest
    {
        [Fact]
        public async Task Delete_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await DeleteOrderAsync(new DeleteOrder { Id = 0 });

            response.CheckResponse(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnDeletedOrder_StatusCode200()
        {
            var response = await Initialize();
            var order = await response.Content.ReadAsAsync<OrderReadDto>();
            
            response = await DeleteOrderAsync(new DeleteOrder { Id = order.Id });
            var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

            response.CheckResponse(HttpStatusCode.OK);
            CheckReturnResult(returnResult, order);
        }
    }
}
