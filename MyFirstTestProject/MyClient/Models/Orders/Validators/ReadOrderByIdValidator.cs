using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Orders.Validators
{
    public class ReadOrderByIdValidator : AbstractValidator<ReadOrderById>
    {
        private readonly IRepository<Order> _repository;
        public ReadOrderByIdValidator(IRepository<Order> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative().WithMessage("Id is negative")
                .Must(OrderExist).WithMessage("Order is not found");
        }
        
        private bool OrderExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }
    }
}
