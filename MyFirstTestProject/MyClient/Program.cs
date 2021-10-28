using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyClient.DataAccess;
using System.Threading.Tasks;
using Refit;
using Microsoft.Extensions.DependencyInjection;
using System;

//https://localhost:44334


namespace MyClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddRefitClient<IClient>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44334/api");
            });

            await builder.Build().RunAsync();
        }
    }
}
