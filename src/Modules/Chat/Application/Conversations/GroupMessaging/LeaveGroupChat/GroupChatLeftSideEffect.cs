using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

public class GroupChatLeftSideEffect(
    GroupChatLeftMessage message,
    IHubContext<ChatHub> hubContext)
    : RealtimeSideEffectBase<GroupChatLeftMessage, ChatHub>
{
    public override GroupChatLeftMessage Message { get; } = message;
    public override IHubContext<ChatHub> HubContext { get; } = hubContext;
}
