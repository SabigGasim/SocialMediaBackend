using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

public record UserDirectMessageId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserDirectMessageId New() => new UserDirectMessageId(Guid.CreateVersion7());
}
