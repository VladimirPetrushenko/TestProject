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

        public class DeleteProductHandler : IRequestHandler<DeleteProduct, Product>
        {
            private readonly IRepository<Product> _repository;

            public DeleteProductHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public async Task<Product> Handle(DeleteProduct request, CancellationToken cancellationToken)
            {
                var product = await _repository.GetByID(request.Id);

                _repository.DeleteItem(product);
                await _repository.SaveChanges();

                return product;
            }
        }
    }
}
