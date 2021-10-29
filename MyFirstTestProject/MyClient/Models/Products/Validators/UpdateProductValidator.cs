using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Products.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdateProduct>
    {
        private readonly IRepository<Product> _repository;
        public UpdatePersonValidator(IRepository<Product> repository)
        {
            RuleFor(c => c).NotNull();
            _repository = repository;
            RuleFor(p => p.Id).NotEmpty().WithMessage("Correct id").Must(ProductExist).WithMessage("Product is not found");
            RuleFor(p => p.Alias).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();            
        }

        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
