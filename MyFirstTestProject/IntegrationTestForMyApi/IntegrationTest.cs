using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyClient.Models.Products;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected string baseRoute = "https://localhost/api/";
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
        }

    }
}
