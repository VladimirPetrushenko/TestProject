using JetBrains.Annotations;
using MediatR;
using MyFirstTestProject.Data;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFirstTestProject.Commands
{
    [UsedImplicitly]
    public class ReadAllProductCommand : IRequest<IEnumerable<Product>>
    {
        public class ReadAllProductCommandHandler : IRequestHandler<ReadAllProductCommand, IEnumerable<Product>>
        {
            private readonly IRepository<Product> _repository;

            public ReadAllProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository ?? throw new ArgumentException(null, nameof(repository));
            }

            public Task<IEnumerable<Product>> Handle(ReadAllProductCommand request, CancellationToken cancellationToken)
            {
                var products = _repository.GetAll();

                return Task.FromResult(products);
            }
        }
    }
}
