using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

public class GroupChatJoinedSideEffect(
    GroupChatJoinedMessage message,
    IHubContext<ChatHub> hubContext)
    : RealtimeSideEffectBase<GroupChatJoinedMessage, ChatHub>
{
    public override GroupChatJoinedMessage Message { get; } = message;
    public override IHubContext<ChatHub> HubContext { get; } = hubContext;
}
