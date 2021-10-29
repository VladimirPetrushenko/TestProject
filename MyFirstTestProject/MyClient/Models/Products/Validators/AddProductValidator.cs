using FluentValidation;

namespace MyClient.Models.Products.Validators
{
    public class AddProductValidator : AbstractValidator<AddProduct>
    {
        public AddProductValidator()
        {
            RuleFor(c => c).NotNull();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Alias).NotEmpty();
            RuleFor(c => c.Type).NotEqual(MyModelAndDatabase.Models.ProductType.None);
        }
    }
}
