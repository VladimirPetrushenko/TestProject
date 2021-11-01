using FluentValidation;
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
            RuleFor(p => p.Id).ShouldNotBeNegative().WithMessage("Id is negative").Must(ProductExist).WithMessage("Person is not found");
        }
        
        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
