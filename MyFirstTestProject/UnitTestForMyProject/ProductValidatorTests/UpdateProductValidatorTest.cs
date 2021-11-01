using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;
using System;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class UpdateProductValidatorTest
    {
        private readonly UpdateProductValidator _validator;

        public UpdateProductValidatorTest()
        {
            _validator = new UpdateProductValidator(new MockProductRepo());
        }

        [Fact]
        public void UpdateProduct_ShouldWork()
        {
            var Product = new UpdateProduct { Id = 1, Alias = "Milk", Name = "Saw product", Type = ProductType.Others };

            var result = _validator.TestValidate(Product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNegative()
        {
            var Product = new UpdateProduct { Id = -1, Alias = "Milk", Name = "Saw product", Type = ProductType.Others };

            var result = _validator.TestValidate(Product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNotFound()
        {
            var Product = new UpdateProduct { Id = 0, Alias = "Milk", Name = "Saw product", Type = ProductType.Others };

            var result = _validator.TestValidate(Product);

            result.ShouldHaveValidationErrorFor(Product => Product.Id);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenAliasIsEmptyOrNull()
        {
            var Product = new UpdateProduct { Id = 1, Alias = null, Name = "Saw product", Type = ProductType.Others };

            var result = _validator.TestValidate(Product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            result.ShouldHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameIsEmptyOrNull()
        {
            var Product = new UpdateProduct { Id = 1, Alias = "Milk", Name = null, Type = ProductType.Others };

            var result = _validator.TestValidate(Product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }


        [Fact]
        public void ShouldHaveErrorWhenTypeIsNone()
        {
            var Product = new UpdateProduct { Id = 1, Alias = "Milk", Name = "Saw product", Type = 0 };

            var result = _validator.TestValidate(Product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldHaveValidationErrorFor(Product => Product.Type);
        }
    }
}
