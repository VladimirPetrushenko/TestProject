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
    public class AddPersonValidatorTest
    {
        private readonly AddPersonValidator _validator;

        public AddPersonValidatorTest()
        {
            _validator = new AddPersonValidator();
        }

        [Theory, AutoData]
        public void AddPersonTest(Generator<AddPerson> addPeopleArray)
        {
            var count = 10000;
            var people = addPeopleArray.Take(count).ToList();
            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);
                CheckingFirstName(result, person);
                CheckingLastName(result, person);
            }
        }

        private static void CheckingFirstName(TestValidationResult<AddPerson> result, AddPerson person)
        {
            if (string.IsNullOrEmpty(person.FirstName) || person.FirstName.Length > 20)
                result.ShouldHaveValidationErrorFor(person => person.FirstName);
            else
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
        }

        private static void CheckingLastName(TestValidationResult<AddPerson> result, AddPerson person)
        {
            if (string.IsNullOrEmpty(person.LastName) || person.LastName.Length > 20)
                result.ShouldHaveValidationErrorFor(person => person.LastName);
            else
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }
    }
}
