using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Chatters;

public record ChatterId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static ChatterId New() => new ChatterId(Guid.NewGuid());
}