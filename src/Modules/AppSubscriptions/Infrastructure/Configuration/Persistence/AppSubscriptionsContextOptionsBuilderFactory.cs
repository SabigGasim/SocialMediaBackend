using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Persistence;

public static class AppSubscriptionsContextOptionsBuilderFactory
{
    public static DbContextOptionsBuilder<SubscriptionsDbContext> Create(string connectionString)
    {
        return new DbContextOptionsBuilder<SubscriptionsDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.AppSubscriptions));
    }

    public static void ConfigureAppSubscriptionsOptionsBuilder(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                Schema.AppSubscriptions));
    }
}
