using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application;
using SocialMediaBackend.Infrastructure;
using SocialMediaBackend.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

using (var scope = app.Services.CreateScope())
{
var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbcontext.Database.Migrate();
dbcontext.Database.EnsureCreated();
    PrintSchema(dbcontext);
}


app.Run();






static void PrintSchema(DbContext context)
{
    var model = context.Model.GetEntityTypes();

    foreach (var entity in model)
    {
        Console.WriteLine($"Entity: {entity.Name}");

        foreach (var property in entity.GetProperties())
        {
            Console.WriteLine($"  Property: {property.Name}, Type: {property.ClrType.Name}");
        }

        foreach (var key in entity.GetKeys())
        {
            Console.WriteLine($"  Primary Key: {string.Join(", ", key.Properties.Select(p => p.Name))}");
        }

        foreach (var foreignKey in entity.GetForeignKeys())
        {
            Console.WriteLine($"  Foreign Key: {string.Join(", ", foreignKey.Properties.Select(p => p.Name))} " +
                              $"-> {foreignKey.PrincipalEntityType.Name}");
        }
    }
}