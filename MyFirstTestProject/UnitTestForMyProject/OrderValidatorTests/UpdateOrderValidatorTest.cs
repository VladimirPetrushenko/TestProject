using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Orders;
using MyClient.Models.Orders.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.OrderValidatorTests
{
    public class UpdateOrderValidatorTest : OrderValidatorTest
    {
        private readonly UpdateOrderValidator _validator;

        public UpdateOrderValidatorTest()
            : base()
        {
            _validator = new UpdateOrderValidator(_repoOrder, _repoPerson, _repoProduct);
        }

        [Theory, AutoData]
        public void UpdateOrderTest(Generator<UpdateOrder> updateOrdersArray)
        {
            var orders = updateOrdersArray.Take(OrdersCount).ToList();

            foreach (var order in orders)
            {
                var result = _validator.TestValidate(order);

                СheckingСonditions(result, order);
            }
        }

        private void СheckingСonditions(TestValidationResult<UpdateOrder> result, UpdateOrder order)
        {
            if (_repoOrder.ItemExists(order.Id).Result)
            {
                result.ShouldNotHaveValidationErrorFor(order => order.Id);

                CheckingPerson(result, order);
                CheckingProduct(result, order);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(order => order.Id);
            }
        }
    }
}
