using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class UpdateProductValidatorTest
    {
        public const int RecordsCount = 30;
        public const int ProductCount = 10000;

        private UpdateProductValidator _validator;
        private readonly Fixture fixture;

        public UpdateProductValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
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

        private static void CheckingAlias(TestValidationResult<UpdateProduct> result, UpdateProduct product)
        {
            if (string.IsNullOrEmpty(product.Alias))
                result.ShouldHaveValidationErrorFor(product => product.Alias);
            else
                result.ShouldNotHaveValidationErrorFor(product => product.Alias);
        }

        private static void CheckingName(TestValidationResult<UpdateProduct> result, UpdateProduct product)
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

        private static void CheckingType(TestValidationResult<UpdateProduct> result, UpdateProduct product)
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

        private static void CheckingId(TestValidationResult<UpdateProduct> result, UpdateProduct product, MockProductRepo repo)
        {
            if (repo.ItemExists(product.Id).Result) 
            { 
                result.ShouldNotHaveValidationErrorFor(product => product.Id);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(product => product.Id);
            }
        }
    }
}
