using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class UpdatePersonValidatorTest : PersonValidatorTest
    {
        private readonly UpdatePersonValidator _validator;

        public UpdatePersonValidatorTest() 
            : base()
        {
            _validator = new UpdatePersonValidator(_repository);
        }

        [Theory, AutoData]
        public void UpdatePersonTest(Generator<UpdatePerson> updatePeopleArray)
        {
            var people = updatePeopleArray.Take(PeopleCount).ToList();

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
    }
}
