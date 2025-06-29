using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.EventBus;

public static class EventBusStartup
{
    public static void Initialize()
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var eventBus = scope.Resolve<IEventBus>();

            eventBus.Subscribe(new InboxIntegrationEventHandler<UserCreatedIntegrationEvent>());
            eventBus.Subscribe(new InboxIntegrationEventHandler<UserDeletedIntegrationEvent>());
        }
    }
}
