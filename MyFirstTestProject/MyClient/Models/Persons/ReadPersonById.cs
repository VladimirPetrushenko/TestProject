using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class ReadPersonById : IRequest<Person>
    {
        public int Id { get; set; }
        public bool IsBlock { get; set; }

        public class ReadPersonByIdHandler : IRequestHandler<ReadPersonById, Person>
        {
            private readonly IRepository<Person> _repository;

            public ReadPersonByIdHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<Person> Handle(ReadPersonById request, CancellationToken cancellationToken)
            {
                var person = _repository.GetByID(request.Id);

                return person;
            }
        }
    }
}
