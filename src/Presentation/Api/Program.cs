using FastEndpoints;
using FastEndpoints.Swagger;
using SocialMediaBackend.Api.Modules.Users;
using SocialMediaBackend.Api.Modules.Feed;
using SocialMediaBackend.Api.Modules.Chat;
using SocialMediaBackend.Api.Middlewares;
using SocialMediaBackend.Api;
using SocialMediaBackend.Modules.Users.Application.Configuration;
using SocialMediaBackend.Modules.Feed.Application.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration;
using Autofac.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Chat.Application.Hubs;
using SocialMediaBackend.Modules.Payments.Application.Configuration;
using SocialMediaBackend.Api.Modules.Payments;
using Stripe;
using SocialMediaBackend.Modules.Payments.Infrastructure;
using SocialMediaBackend.Api.Modules.BuildingBlocks;
using SocialMediaBackend.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = builder.Configuration;
var environment = builder.Environment;
var connectionString = config.GetConnectionString("PostgresConnection")!;

DotNetEnv.Env.Load();
StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("StripeSecretKey") ?? throw new NullReferenceException($"StripeSecretKey was null. please create a .env file in the root of this project with the template shown in .env.example file");
builder.Services.Configure<StripeSettings>(options =>
{
    options.WebHookSecret = Environment.GetEnvironmentVariable("StripeWebhookSecret") ?? throw new NullReferenceException($"StripeWebhookSecret was null. please create a .env file in the root of this project with the template shown in .env.example file");
});

builder.Services.AddFeedModule();
builder.Services.AddUserModule();
builder.Services.AddChatModule();
builder.Services.AddPaymentsModule();
builder.Services.AddBuildingBlocks();
builder.Services.AddApi(config);

var app = builder.Build();

if (!environment.IsEnvironment("Testing"))
{
    var redisConnection = config.GetConnectionString("RedisConnection")!;

    using (var scope = app.Services.CreateScope())
    {
        var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

        await Task.WhenAll(
            UsersStartup.InitializeAsync(builder.Services, connectionString, environment, executionContextAccessor),
            FeedStartup.InitializeAsync(builder.Services, connectionString, environment, executionContextAccessor),
            ChatStartup.InitializeAsync(builder.Services, connectionString, redisConnection, environment, executionContextAccessor),
            PaymentsStartup.InitializeAsync(builder.Services, connectionString, environment)
            );
    }
}

app.UseMiddleware<ExceptionStatusCodeMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapFastEndpoints();

app.MapHub<ChatHub>($"/{ApiEndpoints.ChatHub.Connect}");

app.UseSwaggerGen();

app.Run();

public partial class Program { }