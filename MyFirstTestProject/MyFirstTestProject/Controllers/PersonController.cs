using AutoMapper;
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
        private readonly IPersonRepo _personRepo;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, 
            IPersonRepo personRepo,
            IMapper mapper)
        {
            _logger = logger;
            _personRepo = personRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PersonReadDto>> GetAllPerson()
        {
            var people = _personRepo.GetPeople();
            var peopleDto = _mapper.Map<IEnumerable<PersonReadDto>>(people);

            return Ok(peopleDto);
        }
        
        [HttpGet("{id}", Name = "GetPersonById")]
        public ActionResult<PersonReadDto> GetPersonById(int id)
        {
            var person = _personRepo.GetPersonById(id);
            if(person != null) 
                return Ok(_mapper.Map<PersonReadDto>(person));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PersonReadDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            var person = _mapper.Map<Person>(personCreateDto);
            _personRepo.CreatePerson(person);

            var personReadDto = _mapper.Map<PersonReadDto>(person);

            return CreatedAtRoute(nameof(GetPersonById), new { Id = personReadDto.Id }, personReadDto);
        }

        [HttpPut]
        public ActionResult<PersonReadDto> UpdatePerson(int id, PersonUpdateDto personUpdateDto)
        {
            var person = _personRepo.GetPersonById(id);
            if (person == null)
                return NotFound();

            _mapper.Map(personUpdateDto, person);

            _personRepo.UpdatePerson(person);

            var personReadDto = _mapper.Map<PersonReadDto>(person);

            return CreatedAtRoute(nameof(GetPersonById), new { Id = personReadDto.Id }, personReadDto);
        }

        [HttpDelete]
        public ActionResult DeletePerson(int id)
        {
            var person = _personRepo.GetPersonById(id);
            if (person == null)
                return NotFound();
            _personRepo.DeletePerson(person);

            return NoContent();
        }
    }
}
