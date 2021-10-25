using AutoMapper;
using MyFirstTestProject.Dtos;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Profiles
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
