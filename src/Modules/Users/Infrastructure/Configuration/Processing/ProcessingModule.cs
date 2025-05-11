using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Processing;

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
    }
}
