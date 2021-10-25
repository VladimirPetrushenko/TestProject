using MediatR;
using MyFirstTestProject.Data;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFirstTestProject.Commands
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
                _repository = repository ?? throw new ArgumentException(nameof(repository));
            }

            public Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product();

                product.Alias = request.Alias;
                product.Name = request.Name;
                product.Type = request.Type;

                _repository.CreateItem(product);
                return Task.FromResult(product);
            }
        }
    }
}
