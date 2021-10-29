using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using MyClient.Models.Products;

namespace MyClient.Models.Products.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdateProduct>
    {
        private readonly IRepository<Product> _repository;
        public UpdatePersonValidator(IRepository<Product> repository)
        {
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
