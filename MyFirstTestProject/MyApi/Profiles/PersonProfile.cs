using AutoMapper;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;

namespace MyApi.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonReadDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
        }
    }
}
