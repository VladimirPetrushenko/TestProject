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
        public int ProductCount = 10000;
        private readonly MockPersonRepo validEntity;

        private readonly Fixture fixture;

        public MockPersonRepoTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
            validEntity = fixture.Create<MockPersonRepo>();
        }


        [Theory, AutoData]
        public void UpdateProductTest(Generator<UpdateProduct> updateProductsArray)
        {
            var products = updateProductsArray.Take(ProductCount).ToList();
            var repo = fixture.Create<MockProductRepo>();

            _validator = new UpdateProductValidator(repo);

            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);
                CheckingId(result, product, repo);
                CheckingAlias(result, product);
                CheckingName(result, product);
                CheckingType(result, product);
            }
        }

        


        [Fact]
        public void CreateNewPerson_ShouldWork()
        {
            var expected = validEntity.GetAll().Count() + 1;

            validEntity.CreateItem(new Person { Id = 0, FirstName = "firstName", LastName = "lastName" });

            Assert.Equal(expected, validEntity.GetAll().Count());
        }

        [Fact]
        public void UpdatePerson_ShouldWork()
        { 
            var person = validEntity.GetByID(1).Result;

            person.LastName = "lastName";

            validEntity.UpdateItem(person);

            Assert.Equal("lastName", validEntity.GetByID(person.Id).Result.LastName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeletePeople_ShouldWork(int id)
        {
            var expected = validEntity.GetAll().Count() - 1;

            validEntity.DeleteItem(validEntity.GetByID(id).Result);

            Assert.Equal(expected, validEntity.GetAll().Count());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public void DeletePeople_ShouldFail(int id)
        {
            var person = validEntity.GetByID(id).Result;


            Assert.Throws<ArgumentException>(nameof(person), ()=>validEntity.DeleteItem(person));
        }

        [Theory, AutoData]
        public void UpdateProductTest(Generator<UpdateProduct> updateProductsArray)
        {
            var products = updateProductsArray.Take(ProductCount).ToList();
            var repo = fixture.Create<MockProductRepo>();

            _validator = new UpdateProductValidator(repo);

            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);
                CheckingId(result, product, repo);
                CheckingAlias(result, product);
                CheckingName(result, product);
                CheckingType(result, product);
            }
        }
    }
}
