using FluentValidation;
using MyClient.ValidatorExtensions;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.Models.Orders.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrder>
    {
        private readonly IRepository<Person> _repository;
        private readonly IRepository<Person> _repoPerson;
        private readonly IRepository<Product> _repoProduct;

        public UpdateOrderValidator(IRepository<Person> repository, 
            IRepository<Person> repoPerson, 
            IRepository<Product> repoProduct)
        {
            _repository = repository;
            _repoPerson = repoPerson;
            _repoProduct = repoProduct;
            RuleFor(p => p.Id)
                .ShouldNotBeNegative()
                .Must(OrderExist).WithMessage("Order is not found")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Products).Must(ProductExist).WithMessage("Products is not specified");
                    RuleFor(p => p.Person).Must(PersonExist).WithMessage("Person is not specified");
                });

        }

        private bool OrderExist(int id) => _repository.ItemExists(id).Result;

        private bool PersonExist(int id) => _repoPerson.ItemExists(id).Result;

        private bool ProductExist(List<int> ids) =>
            ! ids.Select(id => _repoProduct.ItemExists(id).Result).Contains(false);
    }
}
