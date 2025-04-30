using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Domain.Users;
using System.Text;
using System.Text.Json;
using Tests.Core.Common.Users;
using Xunit;

namespace Tests.Core.Common;


[Collection("Api & Auth")]
public abstract class TestBase(AuthFixture auth, App app) : FastEndpoints.Testing.TestBase<App>
{
    private readonly AuthFixture _auth = auth;
    private readonly App _app = app;
    private readonly SemaphoreSlim _locker = new(1, 1);
    
    private const string _adminId = "e593a99a-56d0-48ff-b3b9-abed820a8bd1";

    public static string AdminAuthToken { get; private set; } = default!;

    protected override async ValueTask SetupAsync()
    {
        await base.SetupAsync();
        if(AdminAuthToken != null)
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

        await CreateUserIfNotExists();
    }


    private async Task CreateUserIfNotExists()
    {
        var token = TestContext.Current.CancellationToken;

        await using var scope = _app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        
        var userId = new UserId(Guid.Parse(_adminId));

        await _locker.WaitAsync(token);

        var adminExists = await context.Users.AnyAsync(x => x.Id == userId, token);
        if (!adminExists)
        {
            var user = await UserFactory.CreateAsync(isPublic: true, ct: token);
            context.Entry(user).Property(x => x.Id).CurrentValue = userId;
            await context.Users.AddAsync(user, token);
            await context.SaveChangesAsync(token);
        }

        _locker.Release();
    }
}
