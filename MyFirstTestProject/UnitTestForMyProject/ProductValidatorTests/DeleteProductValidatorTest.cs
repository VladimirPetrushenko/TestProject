using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class DeleteProductValidatorTest
    {
        public const int RecordsCount = 30;
        public const int ProductCount = 10000;

        private DeleteProductValidator _validator;
        private readonly Fixture fixture;

        public DeleteProductValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
        }

        [Theory, AutoData]
        public void DeleteProductTest(Generator<DeleteProduct> deleteProductsArray)
        {
            var products = deleteProductsArray.Take(ProductCount).ToList();
            var repo = fixture.Create<MockProductRepo>();

            _validator = new DeleteProductValidator(repo);

            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);
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
}
