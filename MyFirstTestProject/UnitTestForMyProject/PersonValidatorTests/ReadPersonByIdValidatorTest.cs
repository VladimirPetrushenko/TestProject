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
        public const int RecordsCount = 30;
        public const int PeopleCount = 10000;

        private ReadPersonByIdValidator _validator;
        private readonly Fixture fixture;

        public ReadPersonByIdValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
        }
        
        [Theory, AutoData]
        public void ReadPersonByIdTest(Generator<ReadPersonById> readPeopleArray)
        {
            var people = readPeopleArray.Take(PeopleCount).ToList();
            var repository = fixture.Create<MockPersonRepo>();

            _validator = new ReadPersonByIdValidator(repository);
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                if (repository.ItemExists(person.Id).Result && !person.IsBlock)
                {
                    result.ShouldNotHaveValidationErrorFor(person => person.Id);
                }
                else if(repository.ItemExists(person.Id).Result)
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
