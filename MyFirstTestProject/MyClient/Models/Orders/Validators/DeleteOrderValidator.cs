using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Orders.Validators
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrder>
    {
        private readonly IRepository<Order> _repository;
        public DeleteOrderValidator(IRepository<Order> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative()
                .Must(OrderExist).WithMessage("Order is not found");
        }

        private bool OrderExist(int id)
        {
            return _repository.ItemExists(id).Result;
        }
    }
}
