using FluentValidation;

namespace MyClient.Models.Products.Validators
{
    public class AddProductValidator : AbstractValidator<AddProduct>
    {
        public AddProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Alias).NotEmpty();
            RuleFor(c => c.Type).NotEqual(MyModelAndDatabase.Models.ProductType.None);
            RuleFor(c => c.Price).Must(x => x >= 0).WithMessage("Price is not correct");
        }
    }
}
