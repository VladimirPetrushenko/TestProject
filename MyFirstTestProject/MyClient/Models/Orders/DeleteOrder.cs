using AutoMapper;
using MediatR;
using MyClient.Models.Dtos.Orders;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using MyModelAndDatabase.Models.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Orders
{
    public class DeleteOrder : IRequest<OrderReadDto>, IId
    {
        public int Id { get; set; }
        public class DeleteOrderHandler : IRequestHandler<DeleteOrder, OrderReadDto>
        {
            private readonly IRepository<Order> _repository;
            private readonly IMapper _mapper;

            public DeleteOrderHandler(IRepository<Order> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<OrderReadDto> Handle(DeleteOrder request, CancellationToken cancellationToken)
            {
                var order = await _repository.GetByID(request.Id);

                _repository.DeleteItem(order);
                await _repository.SaveChanges();

                var result = _mapper.Map<OrderReadDto>(order);

                return result;
            }
        }
    }
}
