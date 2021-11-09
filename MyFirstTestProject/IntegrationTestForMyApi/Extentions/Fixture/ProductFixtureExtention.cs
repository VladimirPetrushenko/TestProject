using AutoFixture;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;

namespace IntegrationTestForMyApi.Extentions.Fixture
{
    public static class ProductFixtureExtention
    {
        public static AddProduct CreateValideAddProduct(this IFixture fixture)
        {
            var price = fixture.Create<decimal>() + 1;
            return fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .With(p => p.Price, price)
                .Create();
        }

        public static AddProduct CreateAddProductWithoutAlias(this IFixture fixture) =>
            fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .Without(p => p.Alias)
                .Create();
    }
}
