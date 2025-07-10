using Autofac;
using Dapper;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Api.Configuration;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Configuration;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;
using Testcontainers.PostgreSql;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common;

public class App : AppFixture<Program>
{
    private PostgreSqlContainer _container = default!;

    protected override async ValueTask PreSetupAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("social-media-backend-tests")
            .WithUsername("username")
            .WithPassword("password")
            .Build();

        await _container.StartAsync();

    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        base.ConfigureApp(a);
        a.UseEnvironment("Testing");
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        var connectionString = _container.GetConnectionString();

        s.AddDbContext<FakeDbContext>(options =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Fake));
        });

        s.AddScoped<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>, FakeProfileAuthorizationHandler>();

        using (var serviceProvider = s.BuildServiceProvider())
        using (var scope = serviceProvider.CreateScope())
        {
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var contextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            FeedStartup.InitializeAsync(s, connectionString, env, contextAccessor).GetAwaiter().GetResult();
        }

        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<FakeDbContext>();
            var factory = scope.Resolve<IDbConnectionFactory>();
            using (var connection = factory.CreateAsync().GetAwaiter().GetResult())
            {
                var sql = context.Database.GenerateCreateScript();
                connection.Execute(sql);
            }
        }
    }
}
