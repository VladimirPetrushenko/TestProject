using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.Models.Orders.Validators
{
    public class AddOrderValidator : AbstractValidator<AddOrder>
    {
        private readonly IRepository<Person> _repoPerson;
        private readonly IRepository<Product> _repoProduct;

        public AddOrderValidator(IRepository<Person> repoPerson, IRepository<Product> repoProduct)
        {
            _repoPerson = repoPerson;
            _repoProduct = repoProduct;
            RuleFor(p => p.Products).Must(ProductExist).WithMessage("Products is not specified");
            RuleFor(p => p.Person).Must(PersonExist).WithMessage("Person is not specified");
        }

        private bool PersonExist(int id) =>
            _repoPerson.ItemExists(id).Result;

        private bool ProductExist(List<int> ids) =>
            ! ids.Select(id => _repoProduct.ItemExists(id).Result).Contains(false);
    }
}
