using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class ReadProductById : IRequest<Product>
    {
        public int Id { get; set; }

        public class ReadProductByIdHandler : IRequestHandler<ReadProductById, Product>
        {
            private readonly IRepository<Product> _repository;

            public ReadProductByIdHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(ReadProductById request, CancellationToken cancellationToken)
            {
                var product = _repository.GetByID(request.Id);

                return Task.FromResult(product);
            }
        }
    }
}
