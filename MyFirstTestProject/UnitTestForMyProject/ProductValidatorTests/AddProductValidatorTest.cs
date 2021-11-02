using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Models;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class AddProductValidatorTest
    {
        private readonly AddProductValidator _validator;

        public const int RecordsCount = 30;
        public const int ProductCount = 10000;

        public AddProductValidatorTest()
        {
            _validator = new AddProductValidator();
        }

        [Theory, AutoData]
        public void AddProductTest(Generator<AddProduct> addProductArray)
        {
            var products = addProductArray.Take(ProductCount).ToList();
            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);
                CheckingAlias(result, product);
                CheckingName(result, product);
                CheckingType(result, product);
            }
        }

        private static void CheckingAlias(TestValidationResult<AddProduct> result, AddProduct product)
        {
            if (string.IsNullOrEmpty(product.Alias))
                result.ShouldHaveValidationErrorFor(product => product.Alias);
            else
                result.ShouldNotHaveValidationErrorFor(product => product.Alias);
        }

        private static void CheckingName(TestValidationResult<AddProduct> result, AddProduct product)
        {
            if (string.IsNullOrEmpty(product.Name))
                result.ShouldHaveValidationErrorFor(product => product.Name);
            else
                result.ShouldNotHaveValidationErrorFor(product => product.Name);
        }

        private static void CheckingType(TestValidationResult<AddProduct> result, AddProduct product)
        {
            if (product.Type == ProductType.None)
                result.ShouldHaveValidationErrorFor(product => product.Type);
            else
                result.ShouldNotHaveValidationErrorFor(product => product.Type);
        }
    }
}
