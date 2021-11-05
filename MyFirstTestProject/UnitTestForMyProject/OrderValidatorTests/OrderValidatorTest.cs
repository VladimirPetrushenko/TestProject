using AutoFixture;
using FluentValidation.TestHelper;
using MyClient.Models.Orders.Interfaces;
using MyModelAndDatabase.Data;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestForMyProject.OrderValidatorTests
{
    public class OrderValidatorTest
    {
        public int RecordsCount = 30;
        public int OrdersCount = 10000;
        protected readonly MockOrderRepo _repoOrder;
        protected readonly MockPersonRepo _repoPerson;
        protected readonly MockProductRepo _repoProduct;
        protected readonly Fixture fixture;

        public OrderValidatorTest()
        {
            fixture = new Fixture { RepeatCount = RecordsCount };
            _repoOrder = fixture.Create<MockOrderRepo>();
            _repoPerson = fixture.Create<MockPersonRepo>();
            _repoProduct = fixture.Create<MockProductRepo>();
        }

        protected void CheckingProduct<T>(TestValidationResult<T> result, T order)
            where T : IOrder
        {
            if (ProductExist(order.Products))
            {
                result.ShouldNotHaveValidationErrorFor(order => order.Products);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(order => order.Products);
            }
        }

        protected void CheckingPerson<T>(TestValidationResult<T> result, T order)
            where T : IOrder
        {
            if (PersonExist(order.Person))
            {
                result.ShouldNotHaveValidationErrorFor(order => order.Person);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(order => order.Person);
            }
        }

        private bool PersonExist(int id) =>
            _repoPerson.ItemExists(id).Result;

        private bool ProductExist(List<int> ids) =>
            !ids.Select(id => _repoProduct.ItemExists(id).Result).Contains(false);

    }
}
