using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    public class DeletePersonValidator : AbstractValidator<DeletePerson>
    {
        private readonly IRepository<Person> _repository;
        public DeletePersonValidator(IRepository<Person> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative()
                .Must(PersonExist).WithMessage("Person is not found")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Id).Must(PersonIsActive).WithMessage("Person is not active yet");
                });
        }

        private bool PersonExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }

        private bool PersonIsActive(int id)
        {
            return _repository.GetByID(id).Result.IsActive;
        }
    }
}
