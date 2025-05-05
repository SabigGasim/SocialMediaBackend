using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Api;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using SocialMediaBackend.Api.Modules.Users;
using SocialMediaBackend.Api.Modules.Feed;
using SocialMediaBackend.Api.Middlewares;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

builder.Services.AddUserModule(builder.Configuration);
builder.Services.AddFeedModule(builder.Configuration);

builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionStatusCodeMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapFastEndpoints();
app.UseSwaggerGen();

if (!builder.Environment.IsEnvironment("Testing"))
    await using (var scope = app.Services.CreateAsyncScope())
    {
        List<DbContext> contexts = 
        [
            scope.ServiceProvider.GetRequiredService<UsersDbContext>(),
            scope.ServiceProvider.GetRequiredService<FeedDbContext>()
        ];
        
        foreach(var context in contexts)
        {
            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();
            if (builder.Environment.IsDevelopment())
            {
                PrintSchema(context);
            }
        }
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

public partial class Program { }