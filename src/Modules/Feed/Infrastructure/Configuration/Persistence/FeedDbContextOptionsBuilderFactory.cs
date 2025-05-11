using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;
public static class FeedDbContextOptionsBuilderFactory
{
    public static DbContextOptionsBuilder<FeedDbContext> Create(string connectionString)
    {
        return new DbContextOptionsBuilder<FeedDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Feed));
    }

    public static void ConfigureFeedOptionsBuilder(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                Schema.Feed));
    }
}
