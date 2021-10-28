using AutoMapper;
using MyApi.Dtos;
using MyModelAndDatabase.Models;

namespace MyApi.Configuration.Profiles
{
    public class PersonPeofile : Profile
    {
        public PersonPeofile()
        {
            CreateMap<Person, PersonReadDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
        }
    }
}
