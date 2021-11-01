using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using MyModelAndDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class DeletePersonValidatorTest
    {
        private readonly DeletePersonValidator _validator;
        private readonly MockPersonRepo _repository;
        public DeletePersonValidatorTest()
        {
            _repository = new MockPersonRepo();
            _validator = new DeletePersonValidator(_repository);
        }

        [Theory, AutoData]
        public void DeletePersonTest(Generator<DeletePerson> deletePeopleArray)
        {
            var count = 100;
            var people = deletePeopleArray.Take(count).ToList();
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                if (_repository.ItemExists(person.Id).Result && person.IsActive)
                {
                    result.ShouldNotHaveValidationErrorFor(person => person.Id);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(person => person.Id);
                }
            }
        }
    }
}
