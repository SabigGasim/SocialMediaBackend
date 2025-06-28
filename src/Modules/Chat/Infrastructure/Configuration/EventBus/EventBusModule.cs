using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.EventBus;

public class EventBusModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InMemoryEventBusClient>()
            .As<IEventBus>()
            .SingleInstance();
    }
}
