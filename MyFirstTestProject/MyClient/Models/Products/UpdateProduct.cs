using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class UpdateProduct : IRequest<Product>
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public class UpdateProductHandler : IRequestHandler<UpdateProduct, Product>
        {
            private readonly IRepository<Product> _repository;

            public UpdateProductHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public async Task<Product> Handle(UpdateProduct request, CancellationToken cancellationToken)
            {
                var product = await _repository.GetByID(request.Id);

                product.Alias = request.Alias;
                product.Name = request.Name;
                product.Type = request.Type;

                _repository.UpdateItem(product);
                await _repository.SaveChanges();

                return product;
            }
        }
    }
}
