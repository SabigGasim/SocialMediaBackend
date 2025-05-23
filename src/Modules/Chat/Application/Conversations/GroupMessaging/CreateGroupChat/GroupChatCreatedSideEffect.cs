using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public class GroupChatCreatedSideEffect(CreateGroupChatMessage message) : RealtimeSideEffectBase<CreateGroupChatMessage>
{
    public override CreateGroupChatMessage Message { get; } = message;
}
