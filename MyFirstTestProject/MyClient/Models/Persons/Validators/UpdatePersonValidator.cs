using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePerson>
    {
        private readonly IRepository<Person> _repository;
        public UpdatePersonValidator(IRepository<Person> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative()
                .Must(PersonExist).WithMessage("Person is not found")
                .DependentRules(() =>
                {
                    RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is not specified").MustHasLengthBetween(1, 20);
                    RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name is not specified").MustHasLengthBetween(1, 20);
                });

        }

        private bool PersonExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }
    }
}
