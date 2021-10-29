using FluentValidation;

namespace MyClient.Models.Persons.Validators
{
    public class AddPersonValidator : AbstractValidator<AddPerson>
    {
        public AddPersonValidator()
        {
            RuleFor(c => c).NotNull();
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
        }
    }
}
