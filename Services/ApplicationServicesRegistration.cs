using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class ApplicationServicesRegistration
{
    /// <summary>
    /// Adds AutoMapper Services
    /// Adds UserManager Services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
        // services.AddScoped<IServiceManager, ServiceManager>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<Func<IAuthenticationService>>(provider =>
        {
            return provider.GetService<IAuthenticationService>;
        });

        services.AddScoped<Func<IBasketService>>(provider =>
        {
            return provider.GetService<IBasketService>;
        });

        services.AddScoped<Func<IOrderService>>(provider =>
        {
            return provider.GetService<IOrderService>;
        });


        services.AddScoped<Func<IProductService>>(provider =>
        {
            return provider.GetService<IProductService>;
        });

        services.AddScoped<Func<IPaymentService>>(provider =>
        {
            return provider.GetService<IPaymentService>;
        });
        services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        return services;
    }
}