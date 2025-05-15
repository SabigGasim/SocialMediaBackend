using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;

public static class PersistenceStartup
{
    public static async Task InitializeAsync(IWebHostEnvironment env)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<ChatDbContext>();

            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();

            if (env.IsDevelopment())
            {
                var schema = context.Database.GenerateCreateScript();
                Console.WriteLine($"""
                    ----------------------------------------------
                    ------------ ChatDbContext Schema ------------
                    ----------------------------------------------

                    {schema}
                    """);
            }
        }
    }
}
