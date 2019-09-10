using System;
using Business.Interfaces;
using Business.Notifications;
using Business.Services;
using Data.Context;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<StockContext>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}