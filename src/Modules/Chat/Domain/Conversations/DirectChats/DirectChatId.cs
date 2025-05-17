using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

public record DirectChatId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static DirectChatId New() => new DirectChatId(Guid.CreateVersion7());
}
