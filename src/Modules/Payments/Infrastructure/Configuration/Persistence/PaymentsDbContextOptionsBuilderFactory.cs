using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialMediaBackend.Modules.Payments.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Persistence;

public static class PaymentsDbContextOptionsBuilderFactory
{
    public static DbContextOptionsBuilder<PaymentsDbContext> Create(string connectionString)
    {
        return new DbContextOptionsBuilder<PaymentsDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Payments));
    }

    public static void ConfigurePaymentsOptionsBuilder(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                HistoryRepository.DefaultTableName,
                Schema.Payments));
    }
}
