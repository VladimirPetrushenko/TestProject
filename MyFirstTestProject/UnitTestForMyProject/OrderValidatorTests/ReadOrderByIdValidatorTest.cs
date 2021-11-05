using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Orders;
using MyClient.Models.Orders.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.OrderValidatorTests
{
    public class ReadOrderByIdValidatorTest : OrderValidatorTest
    {
        private readonly ReadOrderByIdValidator _validator;

        public ReadOrderByIdValidatorTest()
            : base()
        {
            _validator = new ReadOrderByIdValidator(_repoOrder);
        }

        [Theory, AutoData]
        public void ReadOrderByIdTest(Generator<ReadOrderById> readOrderArray)
        {
            var orders = readOrderArray.Take(OrdersCount).ToList();

            foreach (var order in orders)
            {
                var result = _validator.TestValidate(order);

                if (_repoOrder.ItemExists(order.Id).Result)
                {
                    result.ShouldNotHaveValidationErrorFor(person => person.Id);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(person => person.Id);
                }
            }
        }
    }
}
