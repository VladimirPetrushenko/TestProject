using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;

namespace IntegrationTestForMyApi.Extentions
{
    public static class ProductExtention
    {
        public static void CheckReturnResult(this Product returnResult, Product product)
        {
            returnResult.Id.Should().Be(product.Id);
            returnResult.Alias.Should().Be(product.Alias);
            returnResult.Name.Should().Be(product.Name);
            returnResult.Type.Should().Be(product.Type);
            returnResult.Price.Should().Be(product.Price);
        }

        public static UpdateProduct CreateUpdateProductFromProduct(this Product product) =>
            new UpdateProduct()
            {
                Id = product.Id,
                Alias = product.Alias,
                Name = product.Name,
                Type = product.Type,
                Price = product.Price
            };
    }
}
