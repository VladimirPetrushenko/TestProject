using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyModelAndDatabase.Data.Context;
using System.Net.Http;

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

    }
}
