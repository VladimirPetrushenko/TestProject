using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class AddPerson : IRequest<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class AddPersonHandler : IRequestHandler<AddPerson, Person>
        {
            private readonly IRepository<Person> _repository;

            public AddPersonHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<Person> Handle(AddPerson request, CancellationToken cancellationToken)
            {
                var person = new Person
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                _repository.CreateItem(person);
                _repository.SaveChanges();

                return Task.FromResult(person);
            }
        }
    }
}
