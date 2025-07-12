using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Messaging.Inbox;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.EventBus;

public static class EventBusStartup
{
    public static void Initialize()
    {
        using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var eventBus = scope.Resolve<IEventBus>();

            var factory = scope.Resolve<IDbConnectionFactory>();

            eventBus.Subscribe(new InboxIntegrationEventHandler<UserCreatedIntegrationEvent>(factory));
            eventBus.Subscribe(new InboxIntegrationEventHandler<UserDeletedIntegrationEvent>(factory));
        }
    }
}
