using FluentValidation;

namespace MyClient.ValidatorExtensions
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, int> ShouldNotBeNegative<T>(this IRuleBuilder<T, int> rule)
        {
            return rule.Must(IsNotNegative).WithMessage("Id is negative");
        }

        public static IRuleBuilderOptions<T, string> MustHasLengthBetween<T>(this IRuleBuilder<T, string> rule, int min, int max)
        {
            return rule.Length(min, max).WithMessage("Too long string");
        }

        private static bool IsNotNegative(int id)
        {
            return id >= 0;
        }
    }
}
