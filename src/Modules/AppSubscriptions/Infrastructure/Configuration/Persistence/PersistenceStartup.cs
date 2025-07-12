using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Persistence;

public static class PersistenceStartup
{
    public static async Task InitializeAsync(IWebHostEnvironment env)
    {
        await using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<SubscriptionsDbContext>();

            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();

            if (env.IsDevelopment())
            {
                var schema = context.Database.GenerateCreateScript();
                Console.WriteLine($"""
                    ----------------------------------------------
                    ------------ AppSubscriptionsDbContext Schema ------------
                    ----------------------------------------------

                    {schema}
                    """);
            }
        }
    }
}
