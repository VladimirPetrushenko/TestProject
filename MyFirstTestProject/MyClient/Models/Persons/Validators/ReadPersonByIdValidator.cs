using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    public class ReadPersonByIdValidator : AbstractValidator<ReadPersonById>
    {
        private readonly IRepository<Person> _repository;
        public ReadPersonByIdValidator(IRepository<Person> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative().WithMessage("Id is negative")
                .Must(PersonExist).WithMessage("Person is not found")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Id).Must(PersonIsNotBlock).WithMessage("Person was blocked");
                });
        }
        
        private bool PersonExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }

        private bool PersonIsNotBlock(int id)
        {
            return !_repository.GetByID(id).Result.IsBlock;
        }
    }
}
