using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using MyModelAndDatabase.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class ReadPersonByIdValidatorTest
    {
        private readonly ReadPersonByIdValidator _validator;
        private readonly MockPersonRepo _repository;

        public ReadPersonByIdValidatorTest()
        {
            _repository = new MockPersonRepo();
            _validator = new ReadPersonByIdValidator(_repository);
        }

        [Theory, AutoData]
        public void ReadPersonByIdTest(Generator<ReadPersonById> readPeopleArray)
        {
            var count = 100;
            var people = readPeopleArray.Take(count).ToList();
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                if (_repository.ItemExists(person.Id).Result && !person.IsBlock)
                {
                    result.ShouldNotHaveValidationErrorFor(person => person.Id);
                }
                else if(_repository.ItemExists(person.Id).Result)
                {
                    result.ShouldHaveValidationErrorFor(person => person.IsBlock);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(person => person.Id);
                }
            }
        }
    }
}
