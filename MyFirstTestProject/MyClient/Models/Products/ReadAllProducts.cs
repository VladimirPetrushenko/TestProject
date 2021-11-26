using MediatR;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Products
{
    public class ReadAllProducts : IRequest<IEnumerable<Product>>
    {
        public class ReadAllProductsHandler : IRequestHandler<ReadAllProducts, IEnumerable<Product>>
        {
            private readonly IRepository<Product> _repository;

            public ReadAllProductsHandler(IRepository<Product> repository)
            {
                _repository = repository;
            }

            public Task<IEnumerable<Product>> Handle(ReadAllProducts request, CancellationToken cancellationToken)
            {
                var products = _repository.GetAll().Take(100);

                return Task.FromResult(products);
            }
        }
    }
}
