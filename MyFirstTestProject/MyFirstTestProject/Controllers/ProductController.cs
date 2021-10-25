using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstTestProject.Commands;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id, CancellationToken token)
        {
            Product entity = await _mediator.Send(new ReadProductByIdCommand { Id = id}, token);
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            IEnumerable<Product> entity = await _mediator.Send(new ReadAllProductCommand(), token);
            return Ok(entity.ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]

        public async Task<IActionResult> Post([FromBody] AddProductCommand client, CancellationToken token)
        {
            Product entity = await _mediator.Send(client, token);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }
    }
}
