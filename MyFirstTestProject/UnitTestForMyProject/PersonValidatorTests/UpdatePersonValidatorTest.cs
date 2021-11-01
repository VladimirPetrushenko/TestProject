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
    public class UpdatePersonValidatorTest
    {
        private readonly UpdatePersonValidator _validator;
        private readonly MockPersonRepo _repository;

        public UpdatePersonValidatorTest()
        {
            _repository = new MockPersonRepo();
            _validator = new UpdatePersonValidator(_repository);
        }

        [Theory, AutoData]
        public void UpdatePersonTest(Generator<UpdatePerson> readPeopleArray)
        {
            var count = 10000;
            var people = readPeopleArray.Take(count).ToList();
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                СheckingСonditions(result, person);
            }
        }

        private void СheckingСonditions(TestValidationResult<UpdatePerson> result, UpdatePerson person)
        {
            if(_repository.ItemExists(person.Id).Result && _repository.GetByID(person.Id).Result.IsActive)
            {
                result.ShouldNotHaveValidationErrorFor(person => person.Id);
                CheckingFirstName(result, person);
                CheckingLastName(result, person);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(person => person.Id);
            }
        }

        private void CheckingFirstName(TestValidationResult<UpdatePerson> result, UpdatePerson person)
        {
            if(string.IsNullOrEmpty(person.FirstName) || person.FirstName.Length > 20)
                result.ShouldHaveValidationErrorFor(person => person.FirstName);
            else
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
        }

        private void CheckingLastName(TestValidationResult<UpdatePerson> result, UpdatePerson person)
        {
            if (string.IsNullOrEmpty(person.LastName) || person.LastName.Length > 20)
                result.ShouldHaveValidationErrorFor(person => person.LastName);
            else
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }
    }
}
