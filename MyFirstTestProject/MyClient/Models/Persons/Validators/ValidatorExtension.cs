using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.Models.Persons.Validators
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, int> ShouldNotBeNegative<T>(this IRuleBuilder<T, int> rule)
        {
            return rule.Must(IsNotNegative).WithMessage("Id is negative");
        }

        private static bool IsNotNegative(int id)
        {
            return id >= 0;
        }
    }
}
