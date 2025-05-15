using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;
public static class ChatDbContextOptionsBuilderFactory
{
    public static DbContextOptionsBuilder<ChatDbContext> Create(string connectionString)
    {
        return new DbContextOptionsBuilder<ChatDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Chat));
    }

    public static void ConfigureChatOptionsBuilder(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                Schema.Chat));
    }
}
