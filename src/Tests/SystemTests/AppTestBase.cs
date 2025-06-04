using Autofac;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using System.Text;
using System.Text.Json;

namespace SocialMediaBackend.Tests.SystemTests;

[Collection("Api & Auth")]
public abstract class AppTestBase(AuthFixture auth, App app) : TestBase<App>
{
    private readonly AuthFixture _auth = auth;
    private readonly App _app = app;
    private readonly SemaphoreSlim _locker = new(1, 1);

    private const string _adminId = "e593a99a-56d0-48ff-b3b9-abed820a8bd1";

    public static UserId AdminId { get; } = new UserId(Guid.Parse(_adminId));
    public static string AdminAuthToken { get; private set; } = default!;

    protected override async ValueTask SetupAsync()
    {
        await base.SetupAsync();
        if (AdminAuthToken != null)
        {
            await CreateUserIfNotExists();
            return;
        }

        using var client = _auth.CreateClient(o => o.BaseAddress = new Uri("https://localhost:7272"));
        var body = new
        {
            userid = _adminId,
            email = "sabig@moanyn.com",
            customClaims = new
            {
                admin = "true"
            }
        };

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/token", content, TestContext.Current.CancellationToken);

        AdminAuthToken = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        _app.Client.DefaultRequestHeaders.Authorization = new("Bearer", AdminAuthToken);

        await CreateUserIfNotExists();
    }

    protected static async Task AssertEventually(IProbe probe, TimeSpan timeout)
    {
        await new Poller((int)timeout.TotalMilliseconds).CheckAsync(probe);
    }

    private async Task CreateUserIfNotExists()
    {
        var token = TestContext.Current.CancellationToken;

        await using var scope = UsersCompositionRoot.BeginLifetimeScope();

        var context = scope.Resolve<UsersDbContext>();

        await _locker.WaitAsync(token);

        var adminExists = await context.Users.AnyAsync(x => x.Id == AdminId, token);
        if (!adminExists)
        {
            var user = (await User.CreateAsync(
                username: TextHelper.CreateRandom(8),
                nickname: TextHelper.CreateRandom(8),
                dateOfBirth: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
                userExistsChecker: scope.Resolve<IUserExistsChecker>(),
                ct: token)).Payload;

            context.Entry(user).Property(x => x.Id).CurrentValue = AdminId;
            await context.Users.AddAsync(user, token);
            await context.SaveChangesAsync(token);
        }

        _locker.Release();
    }
}