using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePerson>
    {
        private readonly IRepository<Product> _repository;
        public UpdatePersonValidator(IRepository<Product> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).NotEmpty().WithMessage("Correct id").Must(ProductExist).WithMessage("Product is not found");
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();            
        }

        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
