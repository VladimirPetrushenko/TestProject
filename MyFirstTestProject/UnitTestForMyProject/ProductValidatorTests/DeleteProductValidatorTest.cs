using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using System;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class DeleteProductValidatorTest
    {
        private readonly DeleteProductValidator _validator;

        public DeleteProductValidatorTest()
        {
            _validator = new DeleteProductValidator(new MockProductRepo());
        }

        [Fact]
        public void DeleteProduct_ShouldWork()
        {
            var product = new DeleteProduct { Id = 1 };

            var result = _validator.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNegative()
        {
            var product = new DeleteProduct { Id = -1 };

            var result = _validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNotFound()
        {
            var product = new DeleteProduct { Id = 0 };

            var result = _validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
        }
    }
}
