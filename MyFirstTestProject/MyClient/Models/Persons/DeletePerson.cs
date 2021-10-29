﻿using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Persons
{
    public class DeletePerson : IRequest<Person>
    {
        public int Id { get; set; }

        public class DeletePersonHandler : IRequestHandler<DeletePerson, Person>
        {
            private readonly IRepository<Person> _repository;

            public DeletePersonHandler(IRepository<Person> repository)
            {
                _repository = repository;
            }

            public Task<Person> Handle(DeletePerson request, CancellationToken cancellationToken)
            {
                var person = _repository.GetByID(request.Id);

                _repository.DeleteItem(person);
                _repository.SaveChanges();
                return Task.FromResult(person);
            }
        }
    }
}
