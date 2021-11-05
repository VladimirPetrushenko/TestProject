using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyClient.Models.Persons;
using MyClient.Models.Products;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Models;
using System;
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
        public IntegrationTest()
        {
            var apiFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(servises =>
                    {
                        servises.RemoveAll(typeof(MyContext));
                        servises.AddDbContext<MyContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });

                    });
                });
            TestClient = apiFactory.CreateClient();
            fixture = new Fixture();
        }

        protected AddPerson CreateValideAddPerson()
        {
            var firstName = fixture.Create<string>().Substring(0, 15);
            var lastName = fixture.Create<string>().Substring(0, 15);

            return fixture.Build<AddPerson>()
                .With(p => p.FirstName, firstName)
                .With(p => p.LastName, lastName)
                .With(p => p.IsActive, true)
                .Create();
        }

        public AddProduct CreateValideAddProduct() =>
            fixture.Build<AddProduct>()
                .With(p => p.Type, ProductType.Others)
                .Create();
        
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
    }
}
