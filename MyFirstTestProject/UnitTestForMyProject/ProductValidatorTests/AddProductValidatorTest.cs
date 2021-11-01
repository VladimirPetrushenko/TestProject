using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Models;
using System;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class AddProductValidatorTest
    {
        private readonly AddProductValidator _validator;

        public AddProductValidatorTest()
        {
            _validator = new AddProductValidator();
        }

        [Fact]
        public void AddNewProduct_ShouldWork()
        {
            var product = new AddProduct { Alias = "Milk", Name = "Saw product", Type = ProductType.Main };

            var result = _validator.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenAliasIsNullOrEmpty()
        {
            var product = new AddProduct { Alias = null, Name = "Saw product", Type = ProductType.Main };

            var result = _validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameIsNullOrEmpty()
        {
            var product = new AddProduct { Alias = "Milk", Name = null, Type = ProductType.Main };

            var result = _validator.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldHaveValidationErrorFor(Product => Product.Name);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenTypeIsNone()
        {
            var product = new AddProduct { Alias = "Milk", Name = "Saw product", Type = 0 };

            var result = _validator.TestValidate(product);

            result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
            result.ShouldHaveValidationErrorFor(Product => Product.Type);
        }

        [Fact]
        public void ShouldHaveErrorWhenNameAndAliasAreNullOrEmpty()
        {
            var Product = new AddProduct();

            var result = _validator.TestValidate(Product);

            result.ShouldHaveValidationErrorFor(Product => Product.Alias);
            result.ShouldHaveValidationErrorFor(Product => Product.Name);
            result.ShouldHaveValidationErrorFor(Product => Product.Type);
        }
    }
}
