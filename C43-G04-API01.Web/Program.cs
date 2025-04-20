using C43_G04_API01.Web.Middlewares;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstraction;
using Shared.ErrorModels;

namespace C43_G04_API01.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<StoreDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        builder.Services.AddControllers();
        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            // Func<ActionContext,IActionResult>
            options.InvalidModelStateResponseFactory = (context) =>
            {
                // Get the entries in model state that have validation errors
                var errors = context.ModelState.Where(m =>
                        m.Value.Errors.Any())
                    .Select(e => new ValidationError()
                    {
                        Field = e.Key,
                        Errors = e.Value.Errors.Select(error => error.ErrorMessage)
                    });
                var response = new ValidationErrorResponse() { ValidationErrors = errors };
                return new BadRequestObjectResult(response);
            };
        });

        var app = builder.Build();
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        // app.Use(async (context, next) =>
        // {
        //     Console.WriteLine("Executing request...");
        //     await next.Invoke();
        //     Console.WriteLine("Request completed.");
        //     Console.WriteLine(context.Response);
        // });

        InitializeDbAsync(app);


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        // app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    public static async Task InitializeDbAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
}