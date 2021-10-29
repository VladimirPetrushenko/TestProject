using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    class ReadPersontByIdValidator : AbstractValidator<ReadPersonById>
    {
        private readonly IRepository<Person> _repository;
        public ReadPersontByIdValidator(IRepository<Person> repository)
        {
            RuleFor(c => c).NotNull();
            _repository = repository;
            RuleFor(p => p.Id).NotEmpty().WithMessage("Correct id").Must(ProductExist).WithMessage("Product is not found");
        }
        
        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
