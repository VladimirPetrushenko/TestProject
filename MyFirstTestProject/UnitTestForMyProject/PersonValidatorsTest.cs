using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using MyModelAndDatabase.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject
{
    public class PersonValidatorsTest
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

            [Fact]
            public void ShouldHaveErrorWhenPersonIsNull()
            {
                AddPerson person = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(person));
            }
        }

        public class DeletePersonValidatorTest
        {
            private readonly DeletePersonValidator _validator;

            public DeletePersonValidatorTest()
            {
                _validator = new DeletePersonValidator(new MockPersonRepo());
            }

            [Fact]
            public void DeletePerson_ShouldWork()
            {
                var person = new DeletePerson { Id = 1 };

                var result = _validator.TestValidate(person);

                result.ShouldNotHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNegative()
            {
                var person = new DeletePerson { Id = -1 };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNotFound()
            {
                var person = new DeletePerson { Id = 0 };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenPersonIsNull()
            {
                DeletePerson person = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(person));
            }
        }

        public class ReadPersonByIdValidatorTest
        {
            private readonly ReadPersonByIdValidator _validator;

            public ReadPersonByIdValidatorTest()
            {
                _validator = new ReadPersonByIdValidator(new MockPersonRepo());
            }

            [Fact]
            public void ReadPersonById_ShouldWork()
            {
                var person = new ReadPersonById { Id = 1 };

                var result = _validator.TestValidate(person);

                result.ShouldNotHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNegative()
            {
                var person = new ReadPersonById { Id = -1 };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNotFound()
            {
                var person = new ReadPersonById { Id = 0 };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenPersonIsNull()
            {
                ReadPersonById person = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(person));
            }
        }

        public class UpdatePersonValidatorTest
        {
            private readonly UpdatePersonValidator _validator;

            public UpdatePersonValidatorTest()
            {
                _validator = new UpdatePersonValidator(new MockPersonRepo());
            }

            [Fact]
            public void UpdatePerson_ShouldWork()
            {
                var person = new UpdatePerson { Id = 1, FirstName = "Nat", LastName = "Corye"};

                var result = _validator.TestValidate(person);

                result.ShouldNotHaveValidationErrorFor(person => person.Id);
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNegative()
            {
                var person = new UpdatePerson { Id = -1, FirstName = "Nat", LastName = "Corye" };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNotFound()
            {
                var person = new UpdatePerson { Id = 0, FirstName = "Nat", LastName = "Corye" };

                var result = _validator.TestValidate(person);

                result.ShouldHaveValidationErrorFor(person => person.Id);
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
            }

            [Fact]
            public void ShouldHaveErrorWhenFirstNameIsEmptyOrNull()
            {
                var person = new UpdatePerson { Id = 1, FirstName = null, LastName = "Corye" };

                var result = _validator.TestValidate(person);

                result.ShouldNotHaveValidationErrorFor(person => person.Id);
                result.ShouldHaveValidationErrorFor(person => person.FirstName);
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
            }

            [Fact]
            public void ShouldHaveErrorWhenLastNameIsEmptyOrNull()
            {
                var person = new UpdatePerson { Id = 1, FirstName = "Nat", LastName = null };

                var result = _validator.TestValidate(person);

                result.ShouldNotHaveValidationErrorFor(person => person.Id);
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
                result.ShouldHaveValidationErrorFor(person => person.LastName);
            }

            [Fact]
            public void ShouldHaveErrorWhenPersonIsNull()
            {
                UpdatePerson person = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(person));
            }
        }
    }
}
