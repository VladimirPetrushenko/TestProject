using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class ReadProductByIdValidatorTest : ProductValidatorTest
    {
        private ReadProductByIdValidator _validator;

        public ReadProductByIdValidatorTest()
            : base()
        {
            _validator = new ReadProductByIdValidator(_repository);
        }

        [Theory, AutoData]
        public void ReadProductTest(Generator<ReadProductById> readProductsArray)
        {
            var products = readProductsArray.Take(ProductCount).ToList();

            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);

                if (_repository.ItemExists(product.Id).Result)
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
