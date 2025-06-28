using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

internal sealed class GroupChatLeftSideEffectHandler(
    IHubConnectionTracker connectionTracker,
    IHubContext<ChatHub> hubContext) : IRealtimeSideEffectHandler<GroupChatLeftSideEffect>
{
    private readonly IHubConnectionTracker _connectionTracker = connectionTracker;
    private readonly IHubContext<ChatHub> _hubContext = hubContext;

    public async ValueTask Handle(GroupChatLeftSideEffect notification, CancellationToken cancellationToken)
    {
        var message = notification.Message;
    
        var connections = await _connectionTracker.GetConnectionsAsync(message.MemberId.ToString());
    
        foreach (var connection in connections)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connection, message.GroupId.ToString());
        }
    }
}
