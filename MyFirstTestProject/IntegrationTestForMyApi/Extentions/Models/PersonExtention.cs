using FluentAssertions;
using MyClient.Models.Persons;
using MyModelAndDatabase.Models;

namespace IntegrationTestForMyApi.Extentions
{
    public static class PersonExtention
    {
        public static void CheckReturnResult(this Person returnResult, Person person)
        {
            returnResult.Id.Should().Be(person.Id);
            returnResult.FirstName.Should().Be(person.FirstName);
            returnResult.LastName.Should().Be(person.LastName);
            returnResult.IsActive.Should().Be(person.IsActive);
            returnResult.IsBlock.Should().Be(person.IsBlock);
        }

        public static UpdatePerson CreateUpdatePersonFromPerson(this Person person) =>
            new UpdatePerson()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsActive = person.IsActive,
                IsBlock = person.IsBlock
            };
    }
}
