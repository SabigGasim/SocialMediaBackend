using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;

public static class PersistenceStartup
{
    public static async Task InitializeAsync(IWebHostEnvironment env)
    {
        await using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<FeedDbContext>();

            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();

            if (env.IsDevelopment())
            {
                var schema = context.Database.GenerateCreateScript();
                Console.WriteLine($"""
                    ----------------------------------------------
                    ------------ FeedDbContext Schema ------------
                    ----------------------------------------------

                    {schema}
                    """);
            }
        }
    }
}
