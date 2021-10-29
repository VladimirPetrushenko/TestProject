using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyMediatorModel.Commands.Products.Validators
{
    class ReadProductByIdValidator : AbstractValidator<ReadByIdCommand>
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
