using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Moq;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class DeletePersonValidatorTest
    {
        public const int RecordsCount = 30;
        public const int PeopleCount = 10000;

        private DeletePersonValidator _validator;
        private readonly Fixture fixture;

        public DeletePersonValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
        }

        [Theory, AutoData]
        public void DeletePersonTest(Generator<DeletePerson> deletePeopleArray)
        {
            var people = deletePeopleArray.Take(PeopleCount).ToList();
            var repo = fixture.Create<MockPersonRepo>();

            _validator = new DeletePersonValidator(repo);
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                if (repo.ItemExists(person.Id).Result && repo.GetByID(person.Id).Result.IsActive)
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
