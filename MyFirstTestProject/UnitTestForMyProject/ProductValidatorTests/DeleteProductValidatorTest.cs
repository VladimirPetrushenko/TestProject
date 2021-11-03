using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class DeleteProductValidatorTest : ProductValidatorTest
    {
        private DeleteProductValidator _validator;

        public DeleteProductValidatorTest()
            : base()
        {
            _validator = new DeleteProductValidator(_repository);
        }

        [Theory, AutoData]
        public void DeleteProductTest(Generator<DeleteProduct> deleteProductsArray)
        {
            var products = deleteProductsArray.Take(ProductCount).ToList();

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
