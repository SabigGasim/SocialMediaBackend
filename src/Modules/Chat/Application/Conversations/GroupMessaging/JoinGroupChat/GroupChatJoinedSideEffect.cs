using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

public class GroupChatJoinedSideEffect(GroupChatJoinedMessage message) : RealtimeSideEffectBase<GroupChatJoinedMessage>
{
    public override GroupChatJoinedMessage Message { get; } = message;
}
