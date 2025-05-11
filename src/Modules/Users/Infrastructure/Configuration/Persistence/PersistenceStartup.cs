using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
public static class PersistenceStartup
{
    public static async Task InitializeAsync(IHostEnvironment env)
    {
        await using (var scope = UsersCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<UsersDbContext>();

            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();

            if (env.IsDevelopment())
            {
                var schema = context.Database.GenerateCreateScript();
                Console.WriteLine($"""
                    ----------------------------------------------
                    ------------ UsersDbContext Schema ------------
                    ----------------------------------------------

                    {schema}
                    """);
            }
        }
    }
}
