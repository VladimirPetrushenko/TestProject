using FluentValidation;

namespace MyMediatorModel.Commands.Products.Validators
{
    public class AddProductValidator : AbstractValidator<AddCommand>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Alias).NotEmpty();
            RuleFor(c => c.Type).NotEqual(MyModelAndDatabase.Models.ProductType.None);
        }
    }
}
