using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyMediatorModel.Commands.Products
{
    public class AddCommand : IRequest<Product>
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddCommand, Product>
        {
            private readonly IRepository<Product> _repository;

            public AddProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(AddCommand request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Alias = request.Alias,
                    Name = request.Name,
                    Type = request.Type
                };

                _repository.CreateItem(product);
                return Task.FromResult(product);
            }
        }
    }
}
