using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/PersonQuerable")]
    public class PersonQuerableController : BaseApiController
    {
        public PersonQuerableController(IMediator mediator) : base(mediator)
        {
        }

        //[HttpGet("{id}")]
        //public async Task<Person> Get(int id, CancellationToken token) => await _mediator.Send(new ReadPersonById { Id = id }, token);

        [HttpGet("{name}")]
        public async Task<IQueryable<Person>> GetByName(string name, CancellationToken token) => 
            await _mediator.Send(new ReadPersonByNameQuerable{ Name = name }, token);

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAll(CancellationToken token) => 
            await _mediator.Send(new ReadAllPeople(), token);
    }
}
