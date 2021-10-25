using AutoFixture;
using MyFirstTestProject.Data;
using MyFirstTestProject.Models;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject
{
    public class MockPersonRepoTest
    {
        [Fact]
        public void CreateNewPerson_ShouldWork()
        {
            var validEntity = new MockPersonRepo();

            var expected = validEntity.GetPeople().Count() + 1;

            validEntity.CreatePerson(new Person { Id = 0, FirstName = "firstName", LastName = "lastName" });

            Assert.Equal(expected, validEntity.GetPeople().Count());
        }

        [Fact]
        public void UpdatePerson_ShouldWork()
        {
            var validEntity = new MockPersonRepo();

            var person = validEntity.GetPersonById(1);

            person.LastName = "lastName";

            validEntity.UpdatePerson(person);

            Assert.Equal("lastName", validEntity.GetPersonById(person.Id).LastName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeletePeople_ShouldWork(int id)
        {
            var validEntity = new MockPersonRepo();

            var expected = validEntity.GetPeople().Count() - 1;

            validEntity.DeletePerson(validEntity.GetPersonById(id));

            Assert.Equal(expected, validEntity.GetPeople().Count());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public void DeletePeople_ShouldFail(int id)
        {
            var validEntity = new MockPersonRepo();

            var person = validEntity.GetPersonById(id);

            Assert.Throws<ArgumentException>(nameof(person), ()=>validEntity.DeletePerson(person));
        }


        [Fact]
        public void UsingAutoFixture()
        {
            var validEntity = new MockPersonRepo();

            var person = new Fixture().Create<MockPersonRepo>();
        }
    }
}
