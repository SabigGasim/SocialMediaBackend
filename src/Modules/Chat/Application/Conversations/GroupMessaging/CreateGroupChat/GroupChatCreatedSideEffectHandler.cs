using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

internal sealed class GroupChatCreatedSideEffectHandler(IHubConnectionTracker connectionTracker)
    : IRealtimeSideEffectHandler<GroupChatCreatedSideEffect>
{
    private readonly IHubConnectionTracker _connectionTracker = connectionTracker;

    public async ValueTask Handle(GroupChatCreatedSideEffect notification, CancellationToken cancellationToken)
    {
        var message = notification.Message;
        var groupName = message.Id.ToString();

        var connectionTasks = message.Members
            .Select(x => x.Id.ToString())
            .Select(_connectionTracker.GetConnectionsAsync);

        var users = await Task.WhenAll(connectionTasks);

        var addTasks = users
            .SelectMany(connections => connections)
            .Select(connectionId => notification.HubContext.Groups.AddToGroupAsync(connectionId, groupName));

        await Task.WhenAll(addTasks);
    }
}
