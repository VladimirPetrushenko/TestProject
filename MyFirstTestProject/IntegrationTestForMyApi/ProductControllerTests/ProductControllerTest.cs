﻿using AutoFixture;
using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IntegrationTestForMyApi.Extentions;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class ProductControllerTest : IntegrationTest
    {        
        protected static void CheckReturnResult(Product returnResult, Product product)
        {
            returnResult.Id.Should().Be(product.Id);
            returnResult.Alias.Should().Be(product.Alias);
            returnResult.Name.Should().Be(product.Name);
            returnResult.Type.Should().Be(product.Type);
            returnResult.Price.Should().Be(product.Price);
        }
        
        protected async Task<HttpResponseMessage> DeleteProductAsync(DeleteProduct product) =>
            await TestClient.DeleteAsync(product, Routs.Product);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(Routs.Product, product);

        protected async Task<HttpResponseMessage> UpdateProductAsync(UpdateProduct product) =>
            await TestClient.PutAsJsonAsync(Routs.Product, product);

        protected AddProduct CreateAddProductWithoutAlias() =>
            fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .Without(p => p.Alias)
                .Create();

        protected static UpdateProduct CreateUpdateProductFromProduct(Product product) => 
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
