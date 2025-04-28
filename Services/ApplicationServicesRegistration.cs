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
        services.AddScoped<IServiceManager, ServiceManager>();
        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        return services;
    }
}