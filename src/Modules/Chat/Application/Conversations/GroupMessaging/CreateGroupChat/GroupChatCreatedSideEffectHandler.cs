using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

internal sealed class GroupChatCreatedSideEffectHandler(
    IHubConnectionTracker connectionTracker,
    IHubContext<ChatHub> hubContext) : IRealtimeSideEffectHandler<GroupChatCreatedSideEffect>
{
    private readonly IHubConnectionTracker _connectionTracker = connectionTracker;
    private readonly IHubContext<ChatHub> _hubContext = hubContext;

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
            .Select(connectionId => _hubContext.Groups.AddToGroupAsync(connectionId, groupName));

        await Task.WhenAll(addTasks);
    }
}
