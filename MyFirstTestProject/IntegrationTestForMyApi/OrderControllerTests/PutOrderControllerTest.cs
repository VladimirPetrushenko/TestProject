﻿using MyClient.Models.Dtos.Orders;
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
            response = await TestClient.GetAsync(Routs.Person);
            var person = await response.Content.ReadAsAsync<List<Person>>();

            var updateOrder = new UpdateOrder
            {
                Id = order.Id,
                Person = person.Last().Id,
                Products = order.Products.Select(p => p.Id).ToList()
            };

            response = await TestClient.PutAsJsonAsync(Routs.Order, updateOrder);
            var returnResult = await response.Content.ReadAsAsync<OrderReadDto>();

            CheckResponse(response, HttpStatusCode.OK);
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

            response = await TestClient.PutAsJsonAsync(Routs.Order, updateOrder);

            CheckResponse(response, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_WhenPostNotExistInDataBase_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(Routs.Order, new UpdateOrder { Id = 0 });

            CheckResponse(response, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RequestForWrongRoute_StatusCode404()
        {
            var response = await TestClient.PutAsJsonAsync(Routs.BadRoute, new UpdateOrder());

            CheckResponse(response, HttpStatusCode.NotFound);
        }
    }
}
