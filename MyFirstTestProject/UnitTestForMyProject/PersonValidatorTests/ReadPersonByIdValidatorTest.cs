using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Persons;
using MyClient.Models.Persons.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.PersonValidatorTests
{
    public class ReadPersonByIdValidatorTest : PersonValidatorTest
    {
        private readonly ReadPersonByIdValidator _validator;

        public ReadPersonByIdValidatorTest()
            : base()
        {
            _validator = new ReadPersonByIdValidator(_repository);
        }
        
        [Theory, AutoData]
        public void ReadPersonByIdTest(Generator<ReadPersonById> readPeopleArray)
        {
            var people = readPeopleArray.Take(PeopleCount).ToList();

            foreach (var person in people)
            {
                var result = _validator.TestValidate(person);

                if (_repository.ItemExists(person.Id).Result && !_repository.GetByID(person.Id).Result.IsBlock)
                {
                    result.ShouldNotHaveValidationErrorFor(person => person.Id);
                }
                else if(_repository.ItemExists(person.Id).Result)
                {
                    result.ShouldHaveValidationErrorFor(person => person.Id);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(person => person.Id);
                }
            }
        }
    }
}
