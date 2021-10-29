using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApi.ApiKey.Authentication;
using MyApi.ApiKey.Authorization;
using MyApi.Configuration;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Data.Repositories;
using MyModelAndDatabase.Models;
using System;
using System.Reflection;
using System.Text.Json;

namespace MyApi
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
            services.AddScoped<IRepository<Person>, PersonRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<MyContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

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

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", opt =>
                {
                    opt.AllowAnyOrigin();
                    opt.AllowAnyHeader();
                    opt.AllowAnyMethod();
                });
            });

            services.ConfigureSwaggerFeature();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApi v1"));
            }
            app.UseCors("DefaultPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<ExceptionMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
