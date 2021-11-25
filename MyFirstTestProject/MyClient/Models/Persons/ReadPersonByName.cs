using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class ReadPersonByName : IRequest<IEnumerable<Person>>
    {
        public string Name { get; set; }

        public class ReadPersonByNameHandler : IRequestHandler<ReadPersonByName, IEnumerable<Person>>
        {
            private readonly IRepository<Person> _repository;

            public ReadPersonByNameHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<IEnumerable<Person>> Handle(ReadPersonByName request, CancellationToken cancellationToken)
            {
                var person = _repository.GetAll().Where(x => x.FirstName == request.Name);

                return Task.FromResult(person);
            }
        }
    }
}
