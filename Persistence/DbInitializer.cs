using Domain.Models.Identity;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Persistence;

public class DbInitializer(
    StoreDbContext context,
    StoreIdentityDbContext identityDbContext,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            // Production =>  Create Db + Seeding
            // Dev => Seeding

            // Create Database Code
            // if ((await context.Database.GetPendingMigrationsAsync()).Any())
            // {
            //     await context.Database.MigrateAsync();
            // }

            if (!context.Set<DeliveryMethod>().Any())
            {
                // Read From File
                var data = await File.ReadAllTextAsync("../Persistence/Data/Seeding/delivery.json");

                // Convert to C# code or deserialize
                var objects = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

                if (objects is not null && objects.Any())
                {
                    context.Set<DeliveryMethod>().AddRange(objects);
                    await context.SaveChangesAsync();
                }

                // Save to Db
            }
            if (!context.Set<ProductBrand>().Any())
            {
                // Read From File
                var data = await File.ReadAllTextAsync("../Persistence/Data/Seeding/brands.json");

                // Convert to C# code or deserialize
                var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if (objects is not null && objects.Any())
                {
                    context.Set<ProductBrand>().AddRange(objects);
                    await context.SaveChangesAsync();
                }

                // Save to Db
            }


            if (!context.Set<ProductType>().Any())
            {
                // Read From File
                var data = await File.ReadAllTextAsync("../Persistence/Data/Seeding/types.json");

                // Convert to C# code or deserialize
                var objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                if (objects is not null && objects.Any())
                {
                    context.Set<ProductType>().AddRange(objects);
                    await context.SaveChangesAsync();
                }

                // Save to Db
            }

            if (!context.Set<Product>().Any())
            {
                // Read From File
                var data = await File.ReadAllTextAsync("../Persistence/Data/Seeding/products.json");

                // Convert to C# code or deserialize
                var objects = JsonSerializer.Deserialize<List<Product>>(data);

                if (objects is not null && objects.Any())
                {
                    context.Set<Product>().AddRange(objects);
                    // Save to Db
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task InitializeIdentityAsync()
    {
        // Create Database if it doesn't exist
        // if ((await identityDbContext.Database.GetPendingMigrationsAsync()).Any())
        // {
        //     await identityDbContext.Database.MigrateAsync();
        // }

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        if (!userManager.Users.Any())
        {
            var superAdminUser = new ApplicationUser()
            {
                DisplayName = "Super Admin",
                UserName = "SuperAdmin",
                Email = "SuperAdmin@gmail.com",
                PhoneNumber = "0123456789"
            };

            var adminUser = new ApplicationUser()
            {
                DisplayName = "Admin",
                UserName = "Admin",
                Email = "Admin@gmail.com",
                PhoneNumber = "0123456789"
            };
            await userManager.CreateAsync(superAdminUser, "Passw0rd");
            await userManager.CreateAsync(adminUser, "Passw0rd");

            await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}