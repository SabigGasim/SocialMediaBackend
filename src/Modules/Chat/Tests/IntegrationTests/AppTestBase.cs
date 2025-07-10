using Autofac;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Api;
using SocialMediaBackend.Api.Authentication;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Roles;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests;

[Collection("Api & Auth")]
public abstract class AppTestBase(App app) : TestBase<App>
{
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
            _locker.Release();
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

                context.Chatters.Add(chatter);
                context.Set<ChatterRole>().Add(new ChatterRole(Roles.AdminChatter, chatter.Id));
                await context.SaveChangesAsync(token);
            }
        }
    }

    protected static async Task<ChatterId> CreateChatterAsync(CancellationToken token = default)
    {
        var chatterId = ChatterId.New();
        await EnsureChatterCreated(chatterId, token);
        return chatterId;
    }

    protected async Task<string> CreateUserAndTokenAsync(Guid userId, bool isAdmin = false)
    {
        await EnsureChatterCreated(new ChatterId(userId), TestContext.Current.CancellationToken);

        var body = new TokenGenerationRequest
        {
            UserId = userId,
            Email = "sabig@moanyn.com",
            CustomClaims = new Dictionary<string, object>
            {
                { "admin", isAdmin }
            }
        };

        var (_, token) = await _app.Client.POSTAsync<TokenEndpoint, TokenGenerationRequest, string>(body);

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
            c.Timeout = TimeSpan.FromHours(1);
        });
    }
}