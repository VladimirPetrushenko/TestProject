using AutoFixture;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;

namespace IntegrationTest.Extentions
{
    public static class FuxtureExtention
    {
        public static AddPerson CreateValideAddPerson(this IFixture fixture)
        {
            var firstName = fixture.Create<string>().Substring(0, 15);
            var lastName = fixture.Create<string>().Substring(0, 15);

            return fixture.Build<AddPerson>()
                .With(p => p.FirstName, firstName)
                .With(p => p.LastName, lastName)
                .With(p => p.IsActive, true)
                .Create();
        }

        public static AddProduct CreateValideAddProduct(this IFixture fixture)
        {
            var price = fixture.Create<decimal>() + 1;
            return fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .With(p => p.Price, price)
                .Create();
        }
    }
}
