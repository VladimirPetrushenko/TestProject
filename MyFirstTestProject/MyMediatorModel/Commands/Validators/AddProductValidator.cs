using FluentValidation;

namespace MyMediatorModel.Commands.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Alias).NotEmpty();
            RuleFor(c => c.Type).NotEqual(MyModelAndDatabase.Models.ProductType.None);
        }
    }
}
