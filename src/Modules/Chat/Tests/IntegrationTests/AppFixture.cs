using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Api.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Hubs;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests;

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
        using (var serviceProvider = s.BuildServiceProvider())
        using (var scope = serviceProvider.CreateScope())
        {
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var dbConnection = _databaseContainer.GetConnectionString();
            var redisConnection = _redisContainer.GetConnectionString();

            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var contextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            ChatStartup.InitializeAsync(
                serviceCollection: s, 
                dbConnection, 
                redisConnection, 
                env, 
                contextAccessor).GetAwaiter().GetResult();
        }
    }
}
