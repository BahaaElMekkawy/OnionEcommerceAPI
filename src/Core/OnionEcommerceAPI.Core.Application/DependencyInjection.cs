using Microsoft.Extensions.DependencyInjection;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Basket;
using OnionEcommerceAPI.Core.Application.Mappings;
using OnionEcommerceAPI.Core.Application.Services;

namespace OnionEcommerceAPI.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            //services.AddScoped<IProductService, ProductService>(); any service now will be made manually in the service manger and i only need to register the service manager to the DIC 
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<Func<IBasketService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IBasketService>();
            });

            return services;
        }
    }
}
