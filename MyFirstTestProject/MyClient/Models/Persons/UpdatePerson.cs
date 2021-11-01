using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class UpdatePerson : IRequest<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class UpdatePersonHandler : IRequestHandler<UpdatePerson, Person>
        {
            private readonly IRepository<Person> _repository;

            public UpdatePersonHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public async Task<Person> Handle(UpdatePerson request, CancellationToken cancellationToken)
            {
                var person = await _repository.GetByID(request.Id);

                person.FirstName = request.FirstName;
                person.LastName = request.LastName;

                _repository.UpdateItem(person);
                await _repository.SaveChanges();
                return person;
            }
        }
    }
}
