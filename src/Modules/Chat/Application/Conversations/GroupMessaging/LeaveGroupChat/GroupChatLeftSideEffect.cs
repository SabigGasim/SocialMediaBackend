using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

public class GroupChatLeftSideEffect(GroupChatLeftMessage message) : RealtimeSideEffectBase<GroupChatLeftMessage>
{
    public override GroupChatLeftMessage Message { get; } = message;
}
