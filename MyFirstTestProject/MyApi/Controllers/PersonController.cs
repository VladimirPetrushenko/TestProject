using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/Person")]
    public class PersonController : BaseApiController
    {
        public PersonController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(int id, CancellationToken token) => await _mediator.Send(new ReadPersonById { Id = id }, token);

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAll(CancellationToken token) => await _mediator.Send(new ReadAllPeople(), token);

        [HttpPost]
        public async Task<Person> Post([FromBody] AddPerson client, CancellationToken token) => await _mediator.Send(client, token);

        [HttpPut]
        public async Task<Person> Update([FromBody] UpdatePerson client, CancellationToken token) => await _mediator.Send(client, token);

        [HttpDelete]
        public async Task<Person> Delete([FromBody] DeletePerson client, CancellationToken token) => await _mediator.Send(client, token);
    }
}
