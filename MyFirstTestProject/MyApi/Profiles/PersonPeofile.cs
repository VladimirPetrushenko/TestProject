using AutoMapper;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;

namespace MyApi.Profiles
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
