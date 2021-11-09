using FluentAssertions;
using MyClient.Models.Dtos.Orders;

namespace IntegrationTestForMyApi.Extentions
{
    public static class OrderExtention
    {
        public static void CheckReturnResult(this OrderReadDto returnResult, OrderReadDto order)
        {
            returnResult.Person.FirstName.Should().Be(order.Person.FirstName);
            returnResult.Person.LastName.Should().Be(order.Person.LastName);
            returnResult.Person.IsActive.Should().Be(order.Person.IsActive);
            returnResult.Person.IsBlock.Should().Be(order.Person.IsBlock);
            returnResult.Products.Count.Should().Be(order.Products.Count);
            for (int i = 0; i < returnResult.Products.Count; i++)
            {
                returnResult.Products[i].Alias.Should().Be(order.Products[i].Alias);
                returnResult.Products[i].Name.Should().Be(order.Products[i].Name);
                returnResult.Products[i].Type.Should().Be(order.Products[i].Type);
                returnResult.Products[i].Price.Should().Be(order.Products[i].Price);
            }
        }
    }
}
