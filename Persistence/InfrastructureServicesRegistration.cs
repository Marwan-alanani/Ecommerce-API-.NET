namespace Persistence;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddDbContext<StoreIdentityDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRespository, BasketRepository>();

        services.AddIdentityCore<ApplicationUser>(config
                =>
            {
                config.User.RequireUniqueEmail = true;
                // config.Password.RequiredUniqueChars = 2;
                config.Password.RequiredLength = 5;
                config.Password.RequireLowercase =  false;
                config.Password.RequireUppercase =  false;
                config.Password.RequireDigit =  false;
                config.Password.RequireNonAlphanumeric =  false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        return services;
    }
}