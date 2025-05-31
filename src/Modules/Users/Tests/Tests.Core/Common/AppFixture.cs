using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Users.Application.Configuration;
using Testcontainers.PostgreSql;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

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

        using (var serviceProvider = s.BuildServiceProvider())
        using (var scope = serviceProvider.CreateScope())
        {
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            UsersStartup.InitializeAsync(s, connectionString, env).GetAwaiter().GetResult();
        }
    }
}