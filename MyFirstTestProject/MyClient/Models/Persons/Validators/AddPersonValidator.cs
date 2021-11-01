using FluentValidation;
using MyClient.ValidatorExtensions;

namespace MyClient.Models.Persons.Validators
{
    public class AddPersonValidator : AbstractValidator<AddPerson>
    {
        public AddPersonValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is not specified").MustHasLengthBetween(1, 20);
            RuleFor(p => p.LastName).NotEmpty().WithMessage("First Name is not specified").MustHasLengthBetween(1, 20);
        }
    }
}
