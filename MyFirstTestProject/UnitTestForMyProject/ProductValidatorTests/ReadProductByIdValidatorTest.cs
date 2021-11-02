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
    public class ReadProductByIdValidatorTest
    {
        public const int RecordsCount = 30;
        public const int ProductCount = 10000;

        private ReadProductByIdValidator _validator;
        private readonly Fixture fixture;

        public ReadProductByIdValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
        }

        [Theory, AutoData]
        public void ReadProductTest(Generator<ReadProductById> readProductsArray)
        {
            var products = readProductsArray.Take(ProductCount).ToList();
            var repo = fixture.Create<MockProductRepo>();

            _validator = new ReadProductByIdValidator(repo);

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
