using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public class KickGroupMemberSideEffect(
    KickGroupMemberMessage message,
    IHubContext<ChatHub> hubContext)
    : RealtimeSideEffectBase<KickGroupMemberMessage, ChatHub>
{
    public override KickGroupMemberMessage Message { get; } = message;
    public override IHubContext<ChatHub> HubContext { get; } = hubContext;
}
