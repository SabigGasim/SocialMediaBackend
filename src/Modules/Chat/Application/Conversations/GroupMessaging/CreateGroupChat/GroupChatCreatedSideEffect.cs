using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public class GroupChatCreatedSideEffect(CreateGroupChatMessage message) : RealtimeSideEffectBase<CreateGroupChatMessage>
{
    public override CreateGroupChatMessage Message { get; } = message;
}
