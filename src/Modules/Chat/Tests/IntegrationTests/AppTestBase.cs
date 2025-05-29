using Autofac;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Api;
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
        await EnsureAdminCreated();
    }

    private async Task EnsureAdminCreated()
    {
        await _locker.WaitAsync(TestContext.Current.CancellationToken);

        if (AdminAuthToken is not null)
        {
            return;
        }

        AdminAuthToken = await CreateUserAndTokenAsync(AdminId.Value, isAdmin: true);

        _app.Client.DefaultRequestHeaders.Authorization = new("Bearer", AdminAuthToken);

        _locker.Release();
    }

    protected static async Task EnsureChatterCreated(ChatterId userId, CancellationToken token)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<ChatDbContext>();

            var userExists = await context.Chatters.AnyAsync(x => x.Id == userId, token);
            if (!userExists)
            {
                var chatter = Chatter.Create(
                    userId,
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
        }
    }

    protected async Task<string> CreateUserAndTokenAsync(Guid userId, bool isAdmin = false)
    {
        var body = new
        {
            userid = userId,
            email = $"{userId}@test.com",
            customClaims = new { admin = isAdmin }
        };

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = _auth.CreateClient(o => o.BaseAddress = new Uri("https://localhost:7272"));
        var response = await client.PostAsync("/token", content);
        var token = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        // Ensure user exists in ChatDb
        await EnsureChatterCreated(new ChatterId(userId), TestContext.Current.CancellationToken);

        return token;
    }

    protected HubConnection BuildSignalRClient(string token)
    {
        var baseUrl = _app.Client.BaseAddress!;

        return new HubConnectionBuilder()
            .WithUrl($"{baseUrl}{ApiEndpoints.ChatHub.Connect}", options =>
            {
                options.HttpMessageHandlerFactory = _ => _app.Server.CreateHandler();
                options.AccessTokenProvider = () => Task.FromResult(token)!;
            })
            .WithAutomaticReconnect()
            .Build();
    }

    protected static async Task StartHubClients(params HubConnection[] clients)
    {
        await Task.WhenAll(clients.Select(x => x.StartAsync(TestContext.Current.CancellationToken)));
    }

    protected HttpClient BuildHttpClient(string accessToken)
    {
        return _app.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
        });
    }
}