using C43_G04_API01.Web.Middlewares;
using Persistence;
using Services;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace C43_G04_API01.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder
                =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });
        // Add services to the container.
        builder.Services.AddControllers();
        // Adds all services related with Infrastructure Layer
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Adds all services related with Services Layer
        builder.Services.AddApplicationServices(builder.Configuration);
        // Adds all services related with web application layer
        builder.Services.AddWebApplicationServices(builder.Configuration);

        var app = builder.Build();
        // app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        app.UseCustomExceptionMiddleware();
        // app.Use(async (context, next) =>
        // {
        //     Console.WriteLine("Executing request...");
        //     await next.Invoke();
        //     Console.WriteLine("Request completed.");
        //     Console.WriteLine(context.Response);
        // });

        // InitializeDbAsync(app);
        app.InitializeDatabaseAsync();
        app.UseCors("AllowAll");


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "My E-Commerce API";
                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();
                options.DisplayRequestDuration();
            });
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}