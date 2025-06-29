using Autofac;
using Marten;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;

internal sealed class InboxIntegrationEventHandler<TEvent>
    : IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
{
    public async ValueTask Handle(TEvent notification, CancellationToken cancellationToken)
    {
        var message = InboxMessage.Create(notification);

        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var session = scope.Resolve<IDocumentSession>();
            
            session.Store(message);
            
            await session.SaveChangesAsync(CancellationToken.None);
        }
    }
}
