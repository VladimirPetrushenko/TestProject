using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : BaseApiController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id, CancellationToken token) 
            =>  await _mediator.Send(new ReadProductById { Id = id}, token);
        
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll(CancellationToken token) 
            => await _mediator.Send(new ReadAllProducts(), token);

        [HttpPost]
        public async Task<Product> Post([FromBody] AddProduct client, CancellationToken token) 
            => await _mediator.Send(client, token);

        [HttpPut]
        public async Task<Product> Update([FromBody] UpdateProduct client, CancellationToken token) 
            => await _mediator.Send(client, token);

        [HttpDelete]
        public async Task<Product> Delete([FromBody] DeleteProduct client, CancellationToken token) 
            => await _mediator.Send(client, token);
    }
}
