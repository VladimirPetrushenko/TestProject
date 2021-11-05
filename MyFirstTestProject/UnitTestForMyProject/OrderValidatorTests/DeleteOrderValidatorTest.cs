using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Orders;
using MyClient.Models.Orders.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.OrderValidatorTests
{
    public class DeleteOrderValidatorTest : OrderValidatorTest
    {
        private readonly DeleteOrderValidator _validator;

        public DeleteOrderValidatorTest()
        {
            _validator = new DeleteOrderValidator(_repoOrder);
        }

        [Theory, AutoData]
        public void DeleteOrderTest(Generator<DeleteOrder> deleteOrdersArray)
        {
            var orders = deleteOrdersArray.Take(OrdersCount).ToList();

            foreach (var order in orders)
            {
                var result = _validator.TestValidate(order);

                if (_repoOrder.ItemExists(order.Id).Result)
                {
                    result.ShouldNotHaveValidationErrorFor(order => order.Id);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(order => order.Id);
                }
            }
        }
    }
}
