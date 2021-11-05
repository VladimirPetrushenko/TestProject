using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using MyClient.Models.Orders;
using MyClient.Models.Orders.Validators;
using System.Linq;
using Xunit;

namespace UnitTestForMyProject.OrderValidatorTests
{
    public class AddOrderValidatorTest : OrderValidatorTest
    {
        private readonly AddOrderValidator _validator;

        public AddOrderValidatorTest()
            : base()
        {
            _validator = new AddOrderValidator(_repoPerson, _repoProduct);
        }

        [Theory, AutoData]
        public void AddOrderTest(Generator<AddOrder> addOrdersArray)
        {
            var orders = addOrdersArray.Take(OrdersCount).ToList();

            foreach (var order in orders)
            {
                var result = _validator.TestValidate(order);

                CheckingPerson(result, order);
                CheckingProduct(result, order);
            }
        }
    }
}
