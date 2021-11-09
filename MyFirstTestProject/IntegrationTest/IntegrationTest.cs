using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using MyModelAndDatabase.Data.Context;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class IntegrationTest : IDisposable
    { 
        protected readonly HttpClient TestClient;
        protected string baseRoute = "https://localhost/api/";
        public Fixture fixture = new();
        protected IDbContextTransaction Transaction { get; }
        public IntegrationTest()
        {
            var apiFactory = new WebApplicationFactory<Startup>();
            TestClient = apiFactory.CreateClient();
            var DbContext = (MyContext)apiFactory.Services.GetService(typeof(MyContext));
            Transaction = DbContext.Database.BeginTransaction();
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

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }
        }
    }

    //private readonly TestServer _server;
    //private readonly IServiceProvider _services;
    //protected IDbContextTransaction Transaction { get; }

    //protected readonly HttpClient TestClient;
    //public IntegrationTest()
    //{
    //    var builder = WebHost.CreateDefaultBuilder()
    //        .UseStartup<TestStartup>();
    //    _server = new TestServer(builder);
    //    _services = _server.Host.Services;
    //    TestClient = _server.CreateClient();
    //    var DbContext = GetService<MyContext>();
    //    Transaction = DbContext.Database.BeginTransaction();
    //}

    //protected T GetService<T>() => (T)_services.GetService(typeof(T));
}
