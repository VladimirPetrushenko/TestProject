using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Models.Products;
using MyClient.Models.Persons;

namespace MyApi.Configuration
{
    public static class MediatorConfigurator
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ReadAllProducts));
            services.AddMediatR(typeof(ReadProductById));
            services.AddMediatR(typeof(AddProduct));
            services.AddMediatR(typeof(UpdateProduct));
            services.AddMediatR(typeof(DeleteProduct));
            services.AddMediatR(typeof(ReadAllPeople));
            services.AddMediatR(typeof(ReadPersonById));
            services.AddMediatR(typeof(AddPerson));
            services.AddMediatR(typeof(UpdatePerson));
            services.AddMediatR(typeof(DeletePerson));
        }
    }
}
