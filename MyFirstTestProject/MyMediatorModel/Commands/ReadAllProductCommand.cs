using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyMediatorModel.Commands
{
    public class ReadAllProductCommand : IRequest<IEnumerable<Product>>
    {
        public class ReadAllProductCommandHandler : IRequestHandler<ReadAllProductCommand, IEnumerable<Product>>
        {
            private readonly IRepository<Product> _repository;

            public ReadAllProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<IEnumerable<Product>> Handle(ReadAllProductCommand request, CancellationToken cancellationToken)
            {
                var products = _repository.GetAll();

                return Task.FromResult(products);
            }
        }
    }
}
