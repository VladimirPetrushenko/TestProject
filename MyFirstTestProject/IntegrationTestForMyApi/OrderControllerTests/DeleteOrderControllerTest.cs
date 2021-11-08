using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using MyModelAndDatabase.Data.Context;
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
            using var transaction = context.Database.BeginTransaction();
            var response = await DeleteOrderAsync(new DeleteOrder { Id = 0 });

            CheckResponse(response, HttpStatusCode.NotFound);
            transaction.Rollback();
        }

        [Fact]
        public async Task Delete_ReturnDeletedOrder_StatusCode200()
        {
            context.Database.AutoTransactionsEnabled = false;
            using (var transaction = context.Database.BeginTransaction()) 
            { 
                var response = await Initialize();
                var order = await response.Content.ReadAsAsync<OrderReadDto>();

                response = await DeleteOrderAsync(new DeleteOrder { Id = order.Id });
                var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

                CheckResponse(response, HttpStatusCode.OK);
                CheckReturnResult(returnResult, order);

                await EndOrderTest();
                transaction.Rollback();
            }
        }
    }
}
