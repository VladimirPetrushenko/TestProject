using FluentValidation;
using MyClient.ValidatorExtensions;

namespace MyClient.Models.Persons.Validators
{
    public class AddPersonValidator : AbstractValidator<AddPerson>
    {
        public AddPersonValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First Name is not specified").MustHasLengthBetween(1,20);
            RuleFor(c => c.LastName).NotEmpty().WithMessage("First Name is not specified").MustHasLengthBetween(1, 20);
        }
    }
}
