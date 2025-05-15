using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Processing;

public class ProcessingModule : Module
{
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
    }
}
