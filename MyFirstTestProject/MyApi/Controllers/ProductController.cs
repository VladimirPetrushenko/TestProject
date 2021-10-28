using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMediatorModel.Commands;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id, CancellationToken token) =>  await _mediator.Send(new ReadProductByIdCommand { Id = id}, token);
        

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll(CancellationToken token) => await _mediator.Send(new ReadAllProductCommand(), token);

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]

        public async Task<Product> Post([FromBody] AddProductCommand client, CancellationToken token) => await _mediator.Send(client, token);
    }
}
