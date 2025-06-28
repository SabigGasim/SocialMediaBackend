using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.UpdateChatterInfo;

internal class UserInfoUpdatedIntegrationEventHandler : INotificationHandler<UserInforUpdatedIntegrationEvent>
{
    public async ValueTask Handle(UserInforUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var command = new UpdateChatterInfoCommand(
            notification.Id,
            notification.UserId,
            notification.Username,
            notification.Nickname,
            notification.ProfilePicture,
            notification.ProfileIsPublic
            );

        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            await scheduler.EnqueueAsync(command);
        }
    }
}
