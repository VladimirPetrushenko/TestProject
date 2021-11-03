using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class DeletePersonValidatorTest : PersonValidatorTest
    {
        private readonly DeletePersonValidator _validator;

        public DeletePersonValidatorTest()
        {
            _validator = new DeletePersonValidator(_repository);
        }

        [Theory, AutoData]
        public void DeletePersonTest(Generator<DeletePerson> deletePeopleArray)
        {
            var people = deletePeopleArray.Take(PeopleCount).ToList();

            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);

                if (_repository.ItemExists(person.Id).Result && _repository.GetByID(person.Id).Result.IsActive)
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
