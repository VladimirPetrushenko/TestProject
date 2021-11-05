using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            response = await TestClient.GetAsync(baseRoute + personController);
            var person = 


            var updateOrder = new UpdateOrder
            {
                Id = order.Id,

            }

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));
            var returnResult = await response.Content.ReadAsAsync<Product>();

            CheckResponse(response, HttpStatusCode.OK);
            CheckReturnResult(returnResult, product);

            await EndProductTest(product);
        }

        [Fact]
        public async Task Put_BadModel_WhenPostExistInDataBase_StatusCode400()
        {
            var response = await CreateProductAsync(CreateValideAddProduct());
            var product = await response.Content.ReadAsAsync<Product>();
            product.Type = ProductType.None;

            response = await UpdateProductAsync(CreateUpdateProductFromProduct(product));

            CheckResponse(response, HttpStatusCode.BadRequest);

            await EndProductTest(product);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await UpdateProductAsync(new UpdateProduct { Id = 0 });

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(baseRoute + "something", new UpdateProduct());

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
