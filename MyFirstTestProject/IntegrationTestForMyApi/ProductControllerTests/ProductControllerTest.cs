﻿using AutoFixture;
using FluentAssertions;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.ProductControllerTests
{
    public class ProductControllerTest : IntegrationTest
    {
        protected string controllerName = "Product/";
        
        protected static void CheckReturnResult(Product returnResult, Product product)
        {
            returnResult.Id.Should().Be(product.Id);
            returnResult.Alias.Should().Be(product.Alias);
            returnResult.Name.Should().Be(product.Name);
            returnResult.Type.Should().Be(product.Type);
        }
        
        protected async Task<HttpResponseMessage> DeleteProductAsync(DeleteProduct product)
        {
            HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(product),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(baseRoute + controllerName)
            };

            return await TestClient.SendAsync(request);
        }

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code) =>
            response.StatusCode.Should().Be(code);

        protected async Task<HttpResponseMessage> CreateProductAsync(AddProduct product) =>
            await TestClient.PostAsJsonAsync(baseRoute + controllerName, product);

        protected async Task<HttpResponseMessage> UpdateProductAsync(UpdateProduct product) =>
            await TestClient.PutAsJsonAsync(baseRoute + controllerName, product);

        protected async Task EndProductTest(Product product) =>
            await DeleteProductAsync(new DeleteProduct { Id = product.Id });

        protected AddProduct CreateValideAddProduct() =>
            fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .Create();

        protected AddProduct CreateAddProductWithoutAlias() =>
            fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .Without(p => p.Alias)
                .Create();

        protected static UpdateProduct CreateUpdateProductFromProduct(Product product) => 
            new() { Id = product.Id, Alias = product.Alias, Name = product.Name, Type = product.Type };
    }
}
