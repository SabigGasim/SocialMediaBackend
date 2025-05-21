using Autofac;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using System.Text;
using System.Text.Json;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests;

[Collection("Api & Auth")]
public abstract class AppTestBase(AuthFixture auth, App app) : TestBase<App>
{
    private readonly AuthFixture _auth = auth;
    private readonly App _app = app;
    private readonly SemaphoreSlim _locker = new(1, 1);

    private const string _adminId = "e593a99a-56d0-48ff-b3b9-abed820a8bd1";

    public static ChatterId AdminId { get; } = new ChatterId(Guid.Parse(_adminId));
    public static string AdminAuthToken { get; private set; } = default!;

    protected override async ValueTask SetupAsync()
    {
        await base.SetupAsync();
        if (AdminAuthToken is not null)
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


    private async Task CreateUserIfNotExists()
    {
        var token = TestContext.Current.CancellationToken;

        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<ChatDbContext>();

            await _locker.WaitAsync(token);

            var adminExists = await context.Chatters.AnyAsync(x => x.Id == AdminId, token);
            if (!adminExists)
            {
                var chatter = Chatter.Create(
                    AdminId,
                    username: TextHelper.CreateRandom(8),
                    nickname: TextHelper.CreateRandom(8),
                    Media.Create(Media.DefaultProfilePicture.Url, Media.DefaultProfilePicture.FilePath),
                    profileIsPublic: true,
                    followersCount: 0,
                    followingCount: 0
                    );

                await context.Chatters.AddAsync(chatter, token);
                await context.SaveChangesAsync(token);
            }

            _locker.Release();
        }
    }
}

