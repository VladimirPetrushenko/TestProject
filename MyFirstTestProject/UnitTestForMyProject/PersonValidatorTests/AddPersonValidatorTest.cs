using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class AddPersonValidatorTest : PersonValidatorTest
    {
        private readonly AddPersonValidator _validator;

        public AddPersonValidatorTest()
            : base()
        {
            _validator = new AddPersonValidator();
        }

        [Theory, AutoData]
        public void AddPersonTest(Generator<AddPerson> addPeopleArray)
        {
            var people = addPeopleArray.Take(PeopleCount).ToList();

            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);

                CheckingFirstName(result, person);
                CheckingLastName(result, person);
            }
        }
    }
}
