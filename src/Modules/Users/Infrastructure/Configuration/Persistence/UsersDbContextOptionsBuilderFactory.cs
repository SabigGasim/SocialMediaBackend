using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
public static class UsersDbContextOptionsBuilderFactory
{
    public static DbContextOptionsBuilder<UsersDbContext> Create(string connectionString)
    {
        return new DbContextOptionsBuilder<UsersDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Users));
    }

    public static void ConfigureUsersOptionsBuilder(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                Schema.Users));
    }
}
