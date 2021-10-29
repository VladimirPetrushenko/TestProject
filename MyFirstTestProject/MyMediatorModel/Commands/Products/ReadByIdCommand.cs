using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyMediatorModel.Commands.Products
{
    public class ReadByIdCommand : IRequest<Product>
    {
        public int Id { get; set; }

        public class ReadProductCommandHandler : IRequestHandler<ReadByIdCommand, Product>
        {
            private readonly IRepository<Product> _repository;

            public ReadProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(ReadByIdCommand request, CancellationToken cancellationToken)
            {
                var product = _repository.GetByID(request.Id);

                return Task.FromResult(product);
            }
        }
    }
}
