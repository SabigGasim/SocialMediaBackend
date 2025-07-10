using Autofac;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Api;
using SocialMediaBackend.Api.Authentication;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common;


[Collection("Api & Auth")]
public abstract class AppTestBase(App app) : TestBase<App>
{
    private readonly App _app = app;
    private readonly SemaphoreSlim _locker = new(1, 1);

    private const string _adminId = "e593a99a-56d0-48ff-b3b9-abed820a8bd1";

    public static AuthorId AdminId { get; } = new(Guid.Parse(_adminId));
    public static string AdminAuthToken { get; private set; } = default!;

    protected override async ValueTask SetupAsync()
    {
        await base.SetupAsync();
        if (AdminAuthToken != null)
        {
            await CreateUserIfNotExists();
            return;
        }

        var body = new TokenGenerationRequest
        {
            UserId = Guid.Parse(_adminId),
            Email = "sabig@moanyn.com",
            CustomClaims = new Dictionary<string, object>
            {
                { "admin", true }
            }
        };

        var (_, token) = await _app.Client.POSTAsync<TokenEndpoint, TokenGenerationRequest, string>(body);

        AdminAuthToken = token;

        _app.Client.DefaultRequestHeaders.Authorization = new("Bearer", AdminAuthToken);

        await CreateUserIfNotExists();
    }


    private async Task CreateUserIfNotExists()
    {
        var token = TestContext.Current.CancellationToken;

        await _locker.WaitAsync(token);

        await using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<FakeDbContext>();

            var userExists = await context.Set<Author>().AnyAsync(x => x.Id == AdminId, token);
            if (!userExists)
            {
                var author = Author.Create(
                    AdminId,
                    username: TextHelper.CreateRandom(8),
                    nickname: TextHelper.CreateRandom(8),
                    Media.Create(Media.DefaultProfilePicture.Url, Media.DefaultProfilePicture.FilePath),
                    profileIsPublic: true,
                    followersCount: 0,
                    followingCount: 0
                    );

                await context.Authors.AddAsync(author, token);
                await context.SaveChangesAsync(token);
            }
        }

        _locker.Release();
    }
}
