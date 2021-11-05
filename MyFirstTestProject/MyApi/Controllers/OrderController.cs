using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : BaseApiController
    {
        public OrderController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<OrderReadDto> Get(int id, CancellationToken token) => 
            await _mediator.Send(new ReadOrderById { Id = id }, token);

        [HttpGet]
        public async Task<IEnumerable<OrderReadDto>> GetAll(CancellationToken token) =>
            await _mediator.Send(new ReadAllOrders(), token);

        [HttpPost]
        public async Task<OrderReadDto> Post([FromBody] AddOrder client, CancellationToken token) =>
            await _mediator.Send(client, token);

        [HttpPut]
        public async Task<OrderReadDto> Update([FromBody] UpdateOrder client, CancellationToken token) => 
            await _mediator.Send(client, token);

        [HttpDelete]
        public async Task<OrderReadDto> Delete([FromBody] DeleteOrder client, CancellationToken token) =>
            await _mediator.Send(client, token);
    }
}
