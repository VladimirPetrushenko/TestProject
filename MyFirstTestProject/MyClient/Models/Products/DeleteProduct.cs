using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class DeleteProduct : IRequest<Product>
    {
        public int Id { get; set; }

        public class UpdateProductHandler : IRequestHandler<DeleteProduct, Product>
        {
            private readonly IRepository<Product> _repository;

            public UpdateProductHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(DeleteProduct request, CancellationToken cancellationToken)
            {
                var product = _repository.GetByID(request.Id);

                _repository.DeleteItem(product);
                _repository.SaveChanges();

                return Task.FromResult(product);
            }
        }
    }
}
