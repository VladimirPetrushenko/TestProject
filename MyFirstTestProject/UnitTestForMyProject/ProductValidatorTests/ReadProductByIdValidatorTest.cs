using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using System;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class ReadProductByIdValidatorTest
    {
        private readonly ReadProductByIdValidator _validator;

        public ReadProductByIdValidatorTest()
        {
            _validator = new ReadProductByIdValidator(new MockProductRepo());
        }

        [Fact]
        public void ReadProductById_ShouldWork()
        {
            var product = new ReadProductById { Id = 1 };

            var result = _validator.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNegative()
        {
            var product = new ReadProductById { Id = -1 };

            var result = _validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNotFound()
        {
            var product = new ReadProductById { Id = 0 };

            var result = _validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
        }
    }
}
