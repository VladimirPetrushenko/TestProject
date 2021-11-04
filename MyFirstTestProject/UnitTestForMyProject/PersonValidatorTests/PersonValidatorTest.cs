using AutoFixture;
using FluentValidation.TestHelper;
using MyClient.Models.Persons.Interfaces;
using MyModelAndDatabase.Data;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class PersonValidatorTest
    {
        public int RecordsCount = 30;
        public int PeopleCount = 10000;
        protected readonly MockPersonRepo _repository;
        protected readonly Fixture fixture;

        public PersonValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
            _repository = fixture.Create<MockPersonRepo>();
        }

        protected static void CheckingFirstName<T>(TestValidationResult<T> result, T person)
            where T : IFullName
        {
            if (PersonNameCheck(person.FirstName))
            {
                result.ShouldHaveValidationErrorFor(person => person.FirstName);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
            }
        }

        protected static void CheckingLastName<T>(TestValidationResult<T> result, T person)
            where T : IFullName
        {
            if (PersonNameCheck(person.LastName))
            {
                result.ShouldHaveValidationErrorFor(person => person.LastName);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(person => person.LastName);
            }
        }

        protected static bool PersonNameCheck(string value) =>
            string.IsNullOrEmpty(value) || value.Length > 20;
    }
}
