using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;
using System;
using System.Linq;
using Xunit;
using MyModelAndDatabase.Data.Interfaces;

namespace UnitTestForMyProject
{
    public class MockPersonRepoTest
    {
        [Fact]
        public void CreateNewPerson_ShouldWork()
        {
            var validEntity = new MockPersonRepo();

            var expected = validEntity.GetAll().Count() + 1;

            validEntity.CreateItem(new Person { Id = 0, FirstName = "firstName", LastName = "lastName" });

            Assert.Equal(expected, validEntity.GetAll().Count());
        }

        [Fact]
        public void UpdatePerson_ShouldWork()
        {
            var validEntity = new MockPersonRepo();

            var person = validEntity.GetByID(1);

            person.LastName = "lastName";

            validEntity.UpdateItem(person);

            Assert.Equal("lastName", validEntity.GetByID(person.Id).LastName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeletePeople_ShouldWork(int id)
        {
            var validEntity = new MockPersonRepo();

            var expected = validEntity.GetAll().Count() - 1;

            validEntity.DeleteItem(validEntity.GetByID(id));

            Assert.Equal(expected, validEntity.GetAll().Count());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public void DeletePeople_ShouldFail(int id)
        {
            var validEntity = new MockPersonRepo();

            var person = validEntity.GetByID(id);

            Assert.Throws<ArgumentException>(nameof(person), ()=>validEntity.DeleteItem(person));
        }


    }
}
