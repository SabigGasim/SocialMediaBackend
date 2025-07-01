using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public class GroupChatCreatedSideEffect(
    CreateGroupChatMessage message,
    IHubContext<ChatHub> hubContext) 
    : RealtimeSideEffectBase<CreateGroupChatMessage, ChatHub>
{
    public override CreateGroupChatMessage Message { get; } = message;
    public override IHubContext<ChatHub> HubContext { get; } = hubContext;
}
