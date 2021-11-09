using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApi;
using MyModelAndDatabase.Data.Context;
using System;
using System.Net.Http;

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
