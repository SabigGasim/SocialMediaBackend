using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public class KickGroupMemberSideEffect(KickGroupMemberMessage message)
    : RealtimeSideEffectBase<KickGroupMemberMessage>
{
    public override KickGroupMemberMessage Message { get; } = message;
}
