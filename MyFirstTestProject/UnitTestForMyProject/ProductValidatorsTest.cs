using FluentValidation.TestHelper;
using MyClient.Models.Products;
using MyClient.Models.Products.Validators;
using MyModelAndDatabase.Data;
using MyModelAndDatabase.Models;
using System;
using Xunit;

namespace UnitTestForMyProject
{
    public class ProductValidatorsTest
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

            [Fact]
            public void ShouldHaveErrorWhenProductIsNull()
            {
                AddProduct Product = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(Product));
            }
        }

        public class DeleteProductValidatorTest
        {
            private readonly DeleteProductValidator _validator;

            public DeleteProductValidatorTest()
            {
                _validator = new DeleteProductValidator(new MockProductRepo());
            }

            [Fact]
            public void DeleteProduct_ShouldWork()
            {
                var product = new DeleteProduct { Id = 1 };

                var result = _validator.TestValidate(product);

                result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNegative()
            {
                var product = new DeleteProduct { Id = -1 };

                var result = _validator.TestValidate(product);

                result.ShouldHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNotFound()
            {
                var product = new DeleteProduct { Id = 0 };

                var result = _validator.TestValidate(product);

                result.ShouldHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenProductIsNull()
            {
                DeleteProduct product = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(product));
            }
        }

        public class ReadProductByIdValidatorTest
        {
            private readonly ReadProductByIdValidator _validator;

            public ReadProductByIdValidatorTest()
            {
                _validator = new ReadProductByIdValidator(new MockProductRepo());
            }

            [Fact]
            public void ReadProductById_ShouldWork()
            {
                var product = new ReadProductById { Id = 1 };

                var result = _validator.TestValidate(product);

                result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNegative()
            {
                var product = new ReadProductById { Id = -1 };

                var result = _validator.TestValidate(product);

                result.ShouldHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenIdIsNotFound()
            {
                var product = new ReadProductById { Id = 0 };

                var result = _validator.TestValidate(product);

                result.ShouldHaveValidationErrorFor(Product => Product.Id);
            }

            [Fact]
            public void ShouldHaveErrorWhenProductIsNull()
            {
                ReadProductById product = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(product));
            }
        }

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
                var Product = new UpdateProduct { Id = 1, Alias = "Milk", Name = "Saw product", Type = 0};

                var result = _validator.TestValidate(Product);

                result.ShouldNotHaveValidationErrorFor(Product => Product.Id);
                result.ShouldNotHaveValidationErrorFor(Product => Product.Alias);
                result.ShouldNotHaveValidationErrorFor(Product => Product.Name);
                result.ShouldHaveValidationErrorFor(Product => Product.Type);
            }

            [Fact]
            public void ShouldHaveErrorWhenProductIsNull()
            {
                UpdateProduct Product = null;

                Assert.Throws<ArgumentNullException>(() => _validator.TestValidate(Product));
            }
        }
    }
}
