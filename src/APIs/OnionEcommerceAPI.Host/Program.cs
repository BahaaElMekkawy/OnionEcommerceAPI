using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionEcommerceAPI.API.Extensions;
using OnionEcommerceAPI.API.Services;
using OnionEcommerceAPI.Core.Application;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Mappings;
using OnionEcommerceAPI.Core.Domain.Contracts;
using OnionEcommerceAPI.Host.Middleware;
using OnionEcommerceAPI.Infrastructure.Common;
using OnionEcommerceAPI.Infrastructure.Persistence;
using OnionEcommerceAPI.Infrastructure.Persistence.Data;
using OnionEcommerceAPI.Web;
using OnionEcommerceAPI.Web.Errors;

namespace OnionEcommerceAPI.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = false; //Default 
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                        .SelectMany(p => p.Value!.Errors)
                        .Select(E => E.ErrorMessage);

                        return new BadRequestObjectResult(new ApiModelValidationResponse()
                        {
                            Errors = errors
                        });
                    };
                })
                .AddApplicationPart(WebAssemblyReference.Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped<ICurrentUserService,CurrentUserService>();

            //DependencyInjection.AddPersistenceServices(webApplicationBuilder.Services , webApplicationBuilder.Configuration); call from the static class
            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration); //using as extension method
            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddApplicationServices();
            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Intializations
            await app.InitializeStoreContextAsync();
            #endregion

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //My Custom Middleware for handling the exceptions
            app.UseExceptionHandlingMiddleware();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
