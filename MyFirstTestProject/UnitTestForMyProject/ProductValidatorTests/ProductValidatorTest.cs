using AutoFixture;
using FluentValidation.TestHelper;
using MyClient.Models.Products.Interfaces;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class ProductValidatorTest
    {
        public int RecordsCount = 30;
        public int ProductCount = 10000;
        protected readonly Fixture _fixture;
        protected readonly MockProductRepo _repository;

        public ProductValidatorTest()
        {
            _fixture = new Fixture { RepeatCount = RecordsCount };
            _repository = _fixture.Create<MockProductRepo>();
        }

        protected static void CheckingAlias<T>(TestValidationResult<T> result, T product)
            where T : IProduct
        {
            if (string.IsNullOrEmpty(product.Alias))
            {
                result.ShouldHaveValidationErrorFor(product => product.Alias);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(product => product.Alias);
            }
        }

        protected static void CheckingName<T>(TestValidationResult<T> result, T product)
            where T : IProduct
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                result.ShouldHaveValidationErrorFor(product => product.Name);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(product => product.Name);
            }
        }

        protected static void CheckingType<T>(TestValidationResult<T> result, T product)
            where T : IProduct
        {
            if (product.Type == ProductType.None)
            {
                result.ShouldHaveValidationErrorFor(product => product.Type);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(product => product.Type);
            }
        }
    }
}
