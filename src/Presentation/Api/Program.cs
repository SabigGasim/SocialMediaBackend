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

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = builder.Configuration;
var environment = builder.Environment;
var connectionString = config.GetConnectionString("PostgresConnection")!;

builder.Services.AddFeedModule();
builder.Services.AddUserModule();
builder.Services.AddChatModule();
builder.Services.AddPaymentsModule(environment, connectionString);
builder.Services.AddApi(config);

var app = builder.Build();

if (!environment.IsEnvironment("Testing"))
{
    var redisConnection = config.GetConnectionString("RedisConnection")!;

    await Task.WhenAll(
        UsersStartup.InitializeAsync(builder.Services, connectionString, environment),
        FeedStartup.InitializeAsync(builder.Services, connectionString, environment),
        ChatStartup.InitializeAsync(builder.Services, connectionString, redisConnection, environment),
        PaymentsStartup.InitializeAsync(builder.Services)
        );
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