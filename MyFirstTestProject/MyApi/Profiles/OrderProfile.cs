using AutoMapper;
using MyClient.Models.Dtos.Orders;
using MyModelAndDatabase.Models;

namespace MyApi.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDto>();
        }
    }
}
