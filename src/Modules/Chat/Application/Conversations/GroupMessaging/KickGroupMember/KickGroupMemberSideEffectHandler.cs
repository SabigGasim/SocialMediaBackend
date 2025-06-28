using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

internal sealed class KickGroupMemberSideEffectHandler(
    IHubContext<ChatHub> hubContext,
    IHubConnectionTracker hubConnectionTracker)
    : IRealtimeSideEffectHandler<KickGroupMemberSideEffect>
{
    private readonly IHubContext<ChatHub> _hubContext = hubContext;
    private readonly IHubConnectionTracker _hubConnectionTracker = hubConnectionTracker;

    public async ValueTask Handle(KickGroupMemberSideEffect notification, CancellationToken cancellationToken)
    {
        var userId = notification.Message.ChatterId.ToString();
        var groupId = notification.Message.GroupChatId.ToString();

        var connectionIds = await _hubConnectionTracker.GetConnectionsAsync(userId);

        foreach (var connectionId in connectionIds)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId);
        }
    }
}
