using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Products.Validators
{
    public class ReadProductByIdValidator : AbstractValidator<ReadProductById>
    {
        private readonly IRepository<Product> _repository;
        public ReadProductByIdValidator(IRepository<Product> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).ShouldNotBeNegative().Must(ProductExist).WithMessage("Product is not found");
        }
        
        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }
    }
}
