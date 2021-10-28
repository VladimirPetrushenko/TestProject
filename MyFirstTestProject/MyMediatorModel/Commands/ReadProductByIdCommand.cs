using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMediatorModel.Commands
{
    public class ReadProductByIdCommand : IRequest<Product>
    {
        public int Id { get; set; }

        public class ReadProductCommandHandler : IRequestHandler<ReadProductByIdCommand, Product>
        {
            private readonly IRepository<Product> _repository;

            public ReadProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(ReadProductByIdCommand request, CancellationToken cancellationToken)
            {
                var product = _repository.GetByID(request.Id);

                return Task.FromResult(product);
            }
        }
    }
}
