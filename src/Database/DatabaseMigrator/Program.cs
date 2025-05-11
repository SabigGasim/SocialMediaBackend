using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

builder.Services.AddDbContext<UsersDbContext>(builder => builder.ConfigureUsersOptionsBuilder(connectionString));

builder.Services.AddDbContext<FeedDbContext>(builder => builder.ConfigureFeedOptionsBuilder(connectionString));

var app = builder.Build();

app.Run();
