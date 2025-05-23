﻿using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;
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

    protected override async ValueTask SetupAsync()
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        base.ConfigureApp(a);
        a.UseEnvironment("Testing");
        a.ConfigureTestServices(services =>
        {
            services.RemoveAll<IDbConnectionFactory>();
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(_container.GetConnectionString()));

            services.RemoveAll<UsersDbContext>();
            services.AddDbContext<FakeDbContext>(options =>
            {
                options.UseNpgsql(_container.GetConnectionString());
            });
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseNpgsql(_container.GetConnectionString());
            });

            services.AddScoped<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>, FakeProfileAuthorizationHandler>();
        });
    }
}
