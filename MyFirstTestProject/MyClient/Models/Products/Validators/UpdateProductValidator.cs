using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Products.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProduct>
    {
        private readonly IRepository<Product> _repository;
        public UpdateProductValidator(IRepository<Product> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).ShouldNotBeNegative().Must(ProductExist).WithMessage("Product is not found")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Alias).NotEmpty();
                    RuleFor(p => p.Name).NotEmpty();
                    RuleFor(c => c.Type).NotEqual(ProductType.None);
                    RuleFor(c => c.Price).Must(x => x >= 0).WithMessage("Price is not correct");
                });
        }

        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }
    }
}
