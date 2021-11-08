using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected string baseRoute = "https://localhost/api/";
        public Fixture fixture;
        protected MyContext context;
        public IntegrationTest()
        {
            var apiFactory = new WebApplicationFactory<Startup>();
            var testServer = new TestServer(apiFactory.Services);
            var services = testServer.Host.Services;
            context = (MyContext)services.GetService(typeof(MyContext));
            TestClient = testServer.CreateClient();
            fixture = new Fixture();
        }

        protected AddPerson CreateValideAddPerson()
        {
            var firstName = fixture.Create<string>().Substring(0, 15);
            var lastName = fixture.Create<string>().Substring(0, 15);
            var price = fixture.Create<decimal>();

            return fixture.Build<AddPerson>()
                .With(p => p.FirstName, firstName)
                .With(p => p.LastName, lastName)
                .With(p => p.IsActive, true)
                .Create();
        }

        public AddProduct CreateValideAddProduct()
        {
            var price = fixture.Create<decimal>() + 1;
            return fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .With(p => p.Price, price)
                .Create();
        }
        
        protected async Task<HttpResponseMessage> DeleteAsync<T>(T item, string controllerName)
        {
            HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(item),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(baseRoute + controllerName)
            };

            return await TestClient.SendAsync(request);
        }

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code) =>
            response.StatusCode.Should().Be(code);
    }
}
