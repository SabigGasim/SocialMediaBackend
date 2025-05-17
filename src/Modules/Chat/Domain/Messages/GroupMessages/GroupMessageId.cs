using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

public record GroupMessageId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static GroupMessageId New() => new GroupMessageId(Guid.CreateVersion7());
}