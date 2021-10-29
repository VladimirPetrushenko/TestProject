using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using MyClient.Models.Products;

namespace MyClient.Models.Products.Validators
{
    public class ReadProductByIdValidator : AbstractValidator<ReadProductById>
    {
        private readonly IRepository<Product> _repository;
        public ReadProductByIdValidator(IRepository<Product> repository)
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
