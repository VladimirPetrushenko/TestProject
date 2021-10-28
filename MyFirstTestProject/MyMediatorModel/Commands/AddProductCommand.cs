using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyMediatorModel.Commands
{
    public class AddProductCommand : IRequest<Product>
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
        {
            private readonly IRepository<Product> _repository;

            public AddProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
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
