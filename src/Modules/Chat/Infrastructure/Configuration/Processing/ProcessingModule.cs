using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Infrastructure.InternalCommands;
using StackExchange.Redis;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Processing;

public class ProcessingModule(string connectionString) : Module
{
    private readonly string _connectionString = connectionString;

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DomainEventsDispatcher>()
            .As<IDomainEventsDispatcher>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CommandsScheduler>()
            .As<ICommandsScheduler>()
            .SingleInstance();

        builder.Register(_ => ConnectionMultiplexer.Connect(_connectionString, c => c.AbortOnConnectFail = false))
            .As<IConnectionMultiplexer>()
            .SingleInstance();

        builder.RegisterType<UserLockManager>()
            .As<IUserLockMangaer>()
            .SingleInstance();
    }
}
