using FastEndpoints;
using FastEndpoints.Swagger;
using SocialMediaBackend.Api.Modules.Users;
using SocialMediaBackend.Api.Modules.Feed;
using SocialMediaBackend.Api.Modules.Chat;
using SocialMediaBackend.Api.Middlewares;
using SocialMediaBackend.Api.Modules.BuildingBlocks;
using SocialMediaBackend.Api;
using SocialMediaBackend.Modules.Users.Application.Configuration;
using SocialMediaBackend.Modules.Feed.Application.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = builder.Configuration;
var connectionString = config.GetConnectionString("PostgresConnection")!;
var environment = builder.Environment;

builder.Services.AddFeedModule();
builder.Services.AddUserModule();
builder.Services.AddChatModule();
builder.Services.AddApi(config);

var app = builder.Build();

if (!environment.IsEnvironment("Testing"))
{
await Task.WhenAll(
        UsersStartup.InitializeAsync(builder.Services, connectionString, environment),
        FeedStartup.InitializeAsync(builder.Services, connectionString, environment),
        ChatStartup.InitializeAsync(builder.Services, connectionString, environment)
    );
}

app.UseMiddleware<ExceptionStatusCodeMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapFastEndpoints();
app.UseSwaggerGen();


app.Run();

public partial class Program { }