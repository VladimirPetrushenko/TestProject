using MediatR;
using MyClient.Models.Products.Interfaces;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class AddProduct : IRequest<Product>, IProduct
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public class AddProductHandler : IRequestHandler<AddProduct, Product>
        {
            private readonly IRepository<Product> _repository;

            public AddProductHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public async Task<Product> Handle(AddProduct request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Alias = request.Alias,
                    Name = request.Name,
                    Type = request.Type
                };

                _repository.CreateItem(product);
                await _repository.SaveChanges();
                return product;
            }
        }
    }
}
