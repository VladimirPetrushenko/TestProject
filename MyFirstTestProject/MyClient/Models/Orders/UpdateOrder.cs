using AutoMapper;
using MediatR;
using MyClient.Models.Dtos.Orders;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Orders
{
    public class UpdateOrder : OrderUpdateDto, IRequest<OrderReadDto>
    {
        public class UpdateOrderHandler : IRequestHandler<UpdateOrder, OrderReadDto>
        {
            private readonly IRepository<Order> _repository;
            private readonly IRepository<Product> _repoProduct;
            private readonly IRepository<Person> _repoPerson;

            private readonly Mapper _mapper;
            public UpdateOrderHandler(IRepository<Order> repository, 
                Mapper mapper, 
                IRepository<Person> repoPerson, 
                IRepository<Product> repoProduct)
            {
                _repository = repository;
                _mapper = mapper;
                _repoPerson = repoPerson;
                _repoProduct = repoProduct;
            }

            public async Task<OrderReadDto> Handle(UpdateOrder request, CancellationToken cancellationToken)
            {
                var person = await _repoPerson.GetByID(request.Person);
                var products = _repoProduct.GetAll().Where(p => request.Products.Any(x => x == p.Id));

                var order = await _repository.GetByID(request.Id);
                order.Person = person;
                order.Products = products.ToList();

                _repository.UpdateItem(order);
                await _repository.SaveChanges();

                var result = _mapper.Map<OrderReadDto>(order);

                return result;
            }
        }
    }
}
