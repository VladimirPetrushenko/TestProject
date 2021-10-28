using FluentValidation;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyMediatorModel.Commands.Validators
{
    class ReadProductByIdValidator : AbstractValidator<ReadProductByIdCommand>
    {
        private readonly IRepository<Product> _repository;
        public ReadProductByIdValidator(IRepository<Product> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).NotEmpty().WithMessage("Correct id").Must(ProductExist).WithMessage("Product is not found");
        }
        
        private bool ProductExist(int id)
        {
            return _repository.GetByID(id) == null;
        }
    }
}
