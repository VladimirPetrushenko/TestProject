using AutoFixture;
using AutoFixture.Xunit2;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject
{
    public class MockPersonRepoTest
    {
        public int RecordsCount = 30;
        public int PeopleCount = 10000;
        private readonly MockPersonRepo validEntity;
        private readonly Fixture fixture;

        public MockPersonRepoTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
            validEntity = fixture.Create<MockPersonRepo>();
        }

        [Theory, AutoData]
        public void CreateNewPerson_ShouldWork(Generator<Person> personArray)
        {
            var people = personArray.Take(PeopleCount);
            foreach(var person in people)
            {
                var expected = validEntity.GetAll().Count() + 1;

                validEntity.CreateItem(person);

                Assert.Equal(expected, validEntity.GetAll().Count());
            }
        }

        [Theory, AutoData]
        public void UpdatePerson_ShouldWork(Person somePerson)
        { 
            var person = validEntity.GetAll().FirstOrDefault();
            person.LastName = somePerson.LastName;

            validEntity.UpdateItem(person);

            Assert.Equal(somePerson.LastName, validEntity.GetByID(person.Id).Result.LastName);
        }

        [Theory, AutoData]
        public void DeletePeople(Generator<int> intArray)
        {
            var ids = intArray.Take(PeopleCount);

            foreach(var id in ids) { 
                var expected = validEntity.GetAll().Count() - 1;
                var person = validEntity.GetByID(id).Result;

                if (person != null)
                {
                    validEntity.DeleteItem(person);
                    Assert.Equal(expected, validEntity.GetAll().Count());
                }
                else
                {
                    Assert.Throws<ArgumentException>(nameof(person), () => validEntity.DeleteItem(person));
                }
            }
        }
    }
}
