using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class UpdateProductValidatorTest : ProductValidatorTest
    {
        private readonly UpdateProductValidator _validator;

        public UpdateProductValidatorTest()
            : base()
        {
            _validator = new UpdateProductValidator(_repository);
        }

        [Theory, AutoData]
        public void UpdateProductTest(Generator<UpdateProduct> updateProductsArray)
        {
            var products = updateProductsArray.Take(ProductCount).ToList();

            foreach (var product in products)
            {
                var result = _validator.TestValidate(product);

                if(CheckingId(result, product, _repository)) 
                { 
                    CheckingAlias(result, product);
                    CheckingName(result, product);
                    CheckingType(result, product);
                    CheckingPrice(result, product);
                }
            }
        }

        private static bool CheckingId(TestValidationResult<UpdateProduct> result, UpdateProduct product, MockProductRepo repository)
        {
            if (repository.ItemExists(product.Id).Result) 
            { 
                result.ShouldNotHaveValidationErrorFor(product => product.Id);
                return true;
            }
            else
            {
                result.ShouldHaveValidationErrorFor(product => product.Id);
                return false;
            }
        }
    }
}
