using AutoMapper;
using MediatR;
using MyClient.Models.Dtos.Orders;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyClient.Models.Orders
{
    public class ReadOrderById : IRequest<OrderReadDto>
    {
        public int Id { get; set; }
        public class ReadOrderByIdHandler : IRequestHandler<ReadOrderById, OrderReadDto>
        {
            private readonly IRepository<Order> _repository;
            private readonly Mapper _mapper;

            public ReadOrderByIdHandler(IRepository<Order> repository, Mapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<OrderReadDto> Handle(ReadOrderById request, CancellationToken cancellationToken)
            {
                var order = await _repository.GetByID(request.Id);

                var result = _mapper.Map<OrderReadDto>(order);

                return result;
            }
        }
    }
}
