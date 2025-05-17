using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

public record UserDirectChatId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserDirectChatId New() => new UserDirectChatId(Guid.CreateVersion7());
}
