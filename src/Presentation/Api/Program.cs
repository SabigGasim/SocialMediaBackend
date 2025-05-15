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
var environment = builder.Environment;

builder.Services.AddBuildingBlocks(config);
builder.Services.AddFeedModule();
builder.Services.AddUserModule();
builder.Services.AddChatModule();
builder.Services.AddApi(config);

await Task.WhenAll(
    UsersStartup.InitializeAsync(builder.Services, config, environment),
    FeedStartup.InitializeAsync(builder.Services, config, environment),
    ChatStartup.InitializeAsync(builder.Services, config, environment)
    );

var app = builder.Build();

app.UseMiddleware<ExceptionStatusCodeMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapFastEndpoints();
app.UseSwaggerGen();


app.Run();

public partial class Program { }