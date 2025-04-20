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
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}