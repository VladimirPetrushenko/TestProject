using AutoMapper;
using MediatR;
using MyClient.Models.Dtos.Orders;
using MyClient.Models.Orders.Interfaces;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Orders
{
    public class AddOrder : OrderCreatDto, IRequest<OrderReadDto>, IOrder
    {
        public class AddOrderHandler : IRequestHandler<AddOrder, OrderReadDto>
        {
            private readonly IRepository<Order> _repoOrders;
            private readonly IRepository<Product> _repoProduct;
            private readonly IRepository<Person> _repoPerson;
            private readonly IMapper _mapper;

            public AddOrderHandler(IRepository<Order> repoOrders,
                IRepository<Product> repoProduct,
                IRepository<Person> repoPerson, 
                IMapper mapper)
            {
                _repoOrders = repoOrders;
                _repoProduct = repoProduct;
                _repoPerson = repoPerson;
                _mapper = mapper;
            }

            public async Task<OrderReadDto> Handle(AddOrder request, CancellationToken cancellationToken)
            {
                var person = await _repoPerson.GetByID(request.Person);
                var products = _repoProduct.GetAll().Where(p=> request.Products.Any(x=> x == p.Id));

                var order = new Order
                {
                    Person = person,
                    Products = products.ToList()
                };

                _repoOrders.CreateItem(order);
                await _repoOrders.SaveChanges();

                var result = _mapper.Map<OrderReadDto>(order);

                return result;
            }
        }
    }
}
