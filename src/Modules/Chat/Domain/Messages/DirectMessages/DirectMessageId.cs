using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

public record DirectMessageId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static DirectMessageId New() => new DirectMessageId(Guid.CreateVersion7());
}
