using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

public record UserGroupMessageId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserGroupMessageId New() => new UserGroupMessageId(Guid.CreateVersion7());
}