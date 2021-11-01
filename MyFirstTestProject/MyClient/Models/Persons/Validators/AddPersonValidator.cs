using FluentValidation;

namespace MyClient.Models.Persons.Validators
{
    public class AddPersonValidator : AbstractValidator<AddPerson>
    {
        public AddPersonValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First Name is not specified");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("First Name is not specified");
        }
    }
}
