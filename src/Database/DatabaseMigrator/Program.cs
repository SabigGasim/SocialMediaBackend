using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

builder.Services.AddDbContext<UsersDbContext>(builder => builder.ConfigureUsersOptionsBuilder(connectionString));
builder.Services.AddDbContext<FeedDbContext>(builder => builder.ConfigureFeedOptionsBuilder(connectionString));
builder.Services.AddDbContext<ChatDbContext>(builder => builder.ConfigureChatOptionsBuilder(connectionString));
builder.Services.AddDbContext<SubscriptionsDbContext>(builder => builder.ConfigureAppSubscriptionsOptionsBuilder(connectionString));

var app = builder.Build();

app.Run();
