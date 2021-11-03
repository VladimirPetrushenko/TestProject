using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.ProductValidatorTests
{
    public class AddProductValidatorTest : ProductValidatorTest
    {
        private readonly AddProductValidator _validator;

        public AddProductValidatorTest()
            : base()
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
    }
}
