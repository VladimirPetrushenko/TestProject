﻿using FluentValidation;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models;

namespace MyClient.Models.Persons.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePerson>
    {
        private readonly IRepository<Person> _repository;
        public UpdatePersonValidator(IRepository<Person> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).ShouldNotBeNegative().Must(ProductExist).WithMessage("Product is not found");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is not specified");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name is not specified");            
        }

        private bool ProductExist(int id)
        {
            return _repository.ItemExists(id);
        }
    }
}
