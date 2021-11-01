using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using System;
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

        [Fact]
        public void AddNewPerson_ShouldWork()
        {
            var person = new AddPerson { FirstName = "Valeriya", LastName = "Mayakova" };

            var result = _validator.TestValidate(person);

            result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
            result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsTooLong()
        {
            var person = new AddPerson { FirstName = "This is string more than 20 symbols", LastName = "Mayakova" };

            var result = _validator.TestValidate(person);

            result.ShouldHaveValidationErrorFor(person => person.FirstName);
            result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsTooLong()
        {
            var person = new AddPerson { FirstName = "Valeriya", LastName = "This is string more than 20 symbols" };

            var result = _validator.TestValidate(person);

            result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
            result.ShouldHaveValidationErrorFor(person => person.LastName);
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNullOrEmpty()
        {
            var person = new AddPerson { FirstName = null, LastName = "Mayakova" };

            var result = _validator.TestValidate(person);

            result.ShouldHaveValidationErrorFor(person => person.FirstName);
            result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNullOrEmpty()
        {
            var person = new AddPerson { FirstName = "Valeriya", LastName = null };

            var result = _validator.TestValidate(person);

            result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
            result.ShouldHaveValidationErrorFor(person => person.LastName);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameAndFirstNameAreNullOrEmpty()
        {
            var person = new AddPerson();

            var result = _validator.TestValidate(person);

            result.ShouldHaveValidationErrorFor(person => person.FirstName);
            result.ShouldHaveValidationErrorFor(person => person.LastName);
        }
    }
}
