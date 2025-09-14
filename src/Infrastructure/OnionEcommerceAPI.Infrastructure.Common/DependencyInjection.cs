using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionEcommerceAPI.Core.Domain.Contracts.Infrastructure;
using OnionEcommerceAPI.Infrastructure.Common.Basket_Repository;
using StackExchange.Redis;

namespace OnionEcommerceAPI.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddStackExchangeRedisCache((options) =>
            //{
            //    options.Configuration = configuration.GetConnectionString("Redis");
            //});

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionString!);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();



            return services;
        }
    }
}
