using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.ProductHandlers;
using Domain.ProductAttributes.Factory;
using Domain.Products;
using Domain.ProductSells;
using Domain.ProductSells.EntityValidator;
using Domain.ProductSells.Factory;
using Domain.Service;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureHealthCheck(Configuration);
            var assembly = typeof(CreateProductHandler).GetTypeInfo().Assembly;
            services.AddMediatR(assembly);
            
            services.AddDbContext<ProductSellContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString"), 
                b => b.MigrationsAssembly("EFCore")));
            
            //Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductSellRepository, ProductSellRepository>();
            
            
            //Factories
            services.AddScoped<IProductAttributeFactory, ProductAttributeFactory>();
            services.AddScoped<IProductCombinationFactory, ProductCombinationFactory>();
            
            //Validators
            services.AddScoped<IProductCombinationValidator, ProductCombinationValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHealthChecks("/api/healthy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("Self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ProductSellContext>();
            return services;
        }
    }
}