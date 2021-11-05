using AutoMapper;
using MyModelAndDatabase.Models;
using MyClient.Models.Dtos.Products;

namespace MyApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
