using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyMediatorModel.Commands.Products
{
    public class ReadAllCommand : IRequest<IEnumerable<Product>>
    {
        public class ReadAllProductCommandHandler : IRequestHandler<ReadAllCommand, IEnumerable<Product>>
        {
            private readonly IRepository<Product> _repository;

            public ReadAllProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<IEnumerable<Product>> Handle(ReadAllCommand request, CancellationToken cancellationToken)
            {
                var products = _repository.GetAll();

                return Task.FromResult(products);
            }
        }
    }
}
