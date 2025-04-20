using C43_G04_API01.Web.Factories;
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
        builder.Services.AddControllers();
        // Adds all services related with Infrastructure Layer
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Adds all services related with Services Layer
        builder.Services.AddApplicationServices();
        // Adds all services related with web application layer
        builder.Services.AddWebApplicationServices();


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

}