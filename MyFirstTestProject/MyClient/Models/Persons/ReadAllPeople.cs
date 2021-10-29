using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class ReadAllPeople : IRequest<IEnumerable<Person>>
    {
        public class ReadAllPeopleHandler : IRequestHandler<ReadAllPeople, IEnumerable<Person>>
        {
            private readonly IRepository<Person> _repository;

            public ReadAllPeopleHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<IEnumerable<Person>> Handle(ReadAllPeople request, CancellationToken cancellationToken)
            {
                var people = _repository.GetAll();

                return Task.FromResult(people);
            }
        }
    }
}
