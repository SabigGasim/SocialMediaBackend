using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public record GroupChatId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static GroupChatId New() => new GroupChatId(Guid.CreateVersion7());
}
