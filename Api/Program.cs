using SocialMediaBackend.Application;
using SocialMediaBackend.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

using var scope = app.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbcontext.Database.EnsureCreated();



app.Run();
