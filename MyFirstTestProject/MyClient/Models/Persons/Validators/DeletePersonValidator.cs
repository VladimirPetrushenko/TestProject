using FluentValidation;
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
            RuleFor(p => p.Id).NotEmpty().WithMessage("Correct id").Must(ProductExist).WithMessage("Product is not found");
        }

        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
