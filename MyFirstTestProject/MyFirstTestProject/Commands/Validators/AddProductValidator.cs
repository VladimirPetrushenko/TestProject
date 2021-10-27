using FluentValidation;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Commands.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        [UsedImplicitly]
        public AddProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Alias).NotEmpty();
            RuleFor(c => c.Type).NotEqual(Models.ProductType.None);
        }
    }
}
