using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence.DbInitializers;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;
using OnionEcommerceAPI.Infrastructure.Persistence.Identity;
using OnionEcommerceAPI.Infrastructure.Persistence.Interceptors;

namespace OnionEcommerceAPI.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region StoreContext
            services.AddDbContext<StoreContext>((optionBuilder) => //using the ctor that takes Action of optionsBuilder and set the lifetime to scoped by default
                {
                    optionBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("StoreContext"));
                });

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            services.AddScoped<ISaveChangesInterceptor, CustomSaveChangesInterceptor>();
            #endregion

            #region IdentityContext
            services.AddDbContext<StoreIdentityDbContext>((optionBuilder) => //using the ctor that takes Action of optionsBuilder and set the lifetime to scoped by default
                {
                    optionBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
                });
            services.AddScoped<IStoreIdentityContextInitializer,StoreIdentityContextInitializer>();
            #endregion

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddIdentityCore<ApplicationUser>();
            return services;
        }
    }
}
