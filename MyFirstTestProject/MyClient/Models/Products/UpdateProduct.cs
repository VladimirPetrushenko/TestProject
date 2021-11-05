using MediatR;
using MyClient.Models.Dtos.Products;
using MyClient.Models.Products.Interfaces;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class UpdateProduct : ProductUpdateDto, IRequest<Product>, IProduct
    {
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
                product.Price = request.Price;
                product.Type = request.Type;

                _repository.UpdateItem(product);
                await _repository.SaveChanges();

                return product;
            }
        }
    }
}
