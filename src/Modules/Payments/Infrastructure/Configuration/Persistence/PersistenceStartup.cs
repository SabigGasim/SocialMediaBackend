using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Payments.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Persistence;

public static class PersistenceStartup
{
    public static async Task InitializeAsync(IWebHostEnvironment env)
    {
        await using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<PaymentsDbContext>();

            await context.Database.MigrateAsync();
            await context.Database.EnsureCreatedAsync();

            if (env.IsDevelopment())
            {
                var schema = context.Database.GenerateCreateScript();
                Console.WriteLine($"""
                    ----------------------------------------------
                    ------------ PaymentsDbContext Schema ------------
                    ----------------------------------------------

                    {schema}
                    """);
            }
        }
    }
}
