using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstTestProject.Data;
using MyFirstTestProject.Dtos;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IRepository<Person> _personRepo;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, 
            IRepository<Person> personRepo,
            IMapper mapper)
        {
            _logger = logger;
            _personRepo = personRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize]
        public IEnumerable<PersonReadDto> GetAllPerson()
        {
            var people = _personRepo.GetAll();
            return _mapper.Map<IEnumerable<PersonReadDto>>(people);
        }

        [HttpGet("{id}", Name = "GetPersonById")]
        public PersonReadDto GetPersonById(int id) => _mapper.Map<PersonReadDto>(_personRepo.GetByID(id));

        [HttpPost]
        public ActionResult<PersonReadDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            personCreateDto = null;
            if (personCreateDto == null)
            {
                throw new ArgumentException();
            }
            var person = _mapper.Map<Person>(personCreateDto);
            _personRepo.CreateItem(person);

            var personReadDto = _mapper.Map<PersonReadDto>(person);

            return CreatedAtRoute(nameof(GetPersonById), new { personReadDto.Id }, personReadDto);
        }

        [HttpPut]
        public ActionResult<PersonReadDto> UpdatePerson(int id, PersonUpdateDto personUpdateDto)
        {
            var person = _personRepo.GetByID(id);
            if (person == null) 
            { 
                return NotFound();
            }

            _mapper.Map(personUpdateDto, person);

            _personRepo.UpdateItem(person);

            var personReadDto = _mapper.Map<PersonReadDto>(person);

            return CreatedAtRoute(nameof(GetPersonById), new { personReadDto.Id }, personReadDto);
        }

        [HttpDelete]
        public ActionResult DeletePerson(int id)
        {
            var person = _personRepo.GetByID(id);
            if (person == null)
            { 
                return NotFound();
            }
            _personRepo.DeleteItem(person);

            return NoContent();
        }
    }
}
