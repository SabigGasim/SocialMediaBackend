using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public record UserGroupChatId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserGroupChatId New() => new UserGroupChatId(Guid.CreateVersion7());
}
