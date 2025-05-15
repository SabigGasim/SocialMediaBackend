using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;

public class PersistenceModule(IConfiguration config) : Module
{
    private readonly IConfiguration _config = config;

    protected override void Load(ContainerBuilder builder)
    {
        var connectionString = _config.GetConnectionString("PostgresConnection")!;

        builder.Register(_ => new ChatDbContext(
            ChatDbContextOptionsBuilderFactory
                .Create(connectionString)
                .Options))
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
