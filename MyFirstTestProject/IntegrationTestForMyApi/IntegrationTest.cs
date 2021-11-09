using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyModelAndDatabase.Data.Context;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi
{
    public class IntegrationTest : IDisposable
    { 
        protected readonly HttpClient TestClient;
        public Fixture fixture = new();
        protected IDbContextTransaction Transaction { get; }

        public IntegrationTest()
        {
            var apiFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(MyContext));
                        services.AddDbContext<MyContext>(options =>
                            options.UseSqlServer(Routs.ConnectionStrings),
                            ServiceLifetime.Singleton
                        );
                    });
                });
            TestClient = apiFactory.CreateClient();
            var DbContext = (MyContext)apiFactory.Services.GetService(typeof(MyContext));
            Transaction = DbContext.Database.BeginTransaction();
        }

        protected async Task<HttpResponseMessage> DeleteAsync<T>(T item, string route)
        {
            HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(item),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(route)
            };

            return await TestClient.SendAsync(request);
        }

        protected static void CheckResponse(HttpResponseMessage response, HttpStatusCode code) =>
            response.StatusCode.Should().Be(code);

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }
        }
    }
}
