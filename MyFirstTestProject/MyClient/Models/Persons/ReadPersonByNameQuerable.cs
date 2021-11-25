using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class ReadPersonByNameQuerable : IRequest<IQueryable<Person>>
    {
        public string Name { get; set; }

        public class ReadPersonByNameQuerableHandler : IRequestHandler<ReadPersonByNameQuerable, IQueryable<Person>>
        {
            private readonly IQuerableRepository<Person> _repository;

            public ReadPersonByNameQuerableHandler(IQuerableRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<IQueryable<Person>> Handle(ReadPersonByNameQuerable request, CancellationToken cancellationToken)
            {
                var person = _repository.GetAll().Where(x => x.FirstName == request.Name);

                return Task.FromResult(person);
            }
        }
    }
}
