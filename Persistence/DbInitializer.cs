namespace Persistence;

public class DbInitializer(StoreDbContext context) : IDbInitializer
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
                var data = await File.ReadAllTextAsync("../Persistence/Data/Seeding/product.json");

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
}