using AutoMapper;
using MediatR;
using MyClient.Models.Dtos.Orders;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Orders
{
    public class ReadAllOrders : IRequest<IEnumerable<OrderReadDto>>
    {
        public class ReadAllOrdersHandler : IRequestHandler<ReadAllOrders, IEnumerable<OrderReadDto>>
        {
            private readonly IRepository<Order> _repository;
            private readonly IMapper _mapper;

            public ReadAllOrdersHandler(IRepository<Order> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public Task<IEnumerable<OrderReadDto>> Handle(ReadAllOrders request, CancellationToken cancellationToken)
            {
                var orders = _repository.GetAll();

                var result = _mapper.Map<IEnumerable<OrderReadDto>>(orders);

                return Task.FromResult(result);
            }
        }
    }
}
