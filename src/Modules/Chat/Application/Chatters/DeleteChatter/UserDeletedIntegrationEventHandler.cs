using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.DeleteChatter;

internal sealed class UserDeletedIntegrationEventHandler : INotificationHandler<UserDeletedIntegrationEvent>
{
    public async ValueTask Handle(UserDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new DeleteChatterCommand(notification.Id, new ChatterId(notification.UserId));

            await scheduler.EnqueueAsync(command);
        }
    }
}
