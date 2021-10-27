using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyFirstTestProject.Data;
using MyFirstTestProject.Features.Authentication;
using MyFirstTestProject.Features.Authorization;
using MyFirstTestProject.Features.Exceptions;
using MyFirstTestProject.Features.Swagger;
using MyFirstTestProject.Models;
using MyFirstTestProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFirstTestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRepository<Person>, MockPersonRepo>();
            services.AddScoped<IRepository<Product>, MockProductRepo>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            }).AddApiKeySupport(options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.OnlyEmployees, policy => policy.Requirements.Add(new OnlyEmployeesRequirement()));
                options.AddPolicy(Policies.OnlyManagers, policy => policy.Requirements.Add(new OnlyManagersRequirement()));
                options.AddPolicy(Policies.OnlyThirdParties, policy => policy.Requirements.Add(new OnlyThirdPartiesRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, OnlyEmployeesAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, OnlyManagersAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, OnlyThirdPartiesAuthorizationHandler>();

            services.AddSingleton<IGetApiKeyQuery, InMemoryGetApiKeyQuery>();

            services.AddRouting(x => x.LowercaseUrls = true);
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.ConfigureSwaggerFeature();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFirstTestProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
