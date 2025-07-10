using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Api.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration;
using SocialMediaBackend.Modules.Feed.Application.Configuration;
using SocialMediaBackend.Modules.Users.Application.Configuration;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

public class App : AppFixture<Program>
{
    private PostgreSqlContainer _databaseContainer = default!;
    private RedisContainer _redisContainer = default!;

    protected override async ValueTask PreSetupAsync()
    {
        _databaseContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("social-media-backend-tests")
            .WithUsername("username")
            .WithPassword("password")
            .Build();

        _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        await _databaseContainer.StartAsync();
        await _redisContainer.StartAsync();

    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        base.ConfigureApp(a);
        a.UseEnvironment("Testing");
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        var connectionString = _databaseContainer.GetConnectionString();
        var redistConnection = _redisContainer.GetConnectionString();

        using (var serviceProvider = s.BuildServiceProvider())
        using (var scope = serviceProvider.CreateScope())
        {
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var contextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            UsersStartup.InitializeAsync(s, connectionString, env, contextAccessor).GetAwaiter().GetResult();
            ChatStartup.InitializeAsync(s, connectionString, redistConnection, env, contextAccessor).GetAwaiter().GetResult();
            FeedStartup.InitializeAsync(s, connectionString, env, contextAccessor).GetAwaiter().GetResult();
        }
    }
}