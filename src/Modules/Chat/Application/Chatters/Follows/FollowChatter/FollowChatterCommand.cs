using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.Follows.FollowChatter;

[method: JsonConstructor]
public class FollowChatterCommand(
    Guid id,
    ChatterId followerId,
    ChatterId chatterId,
    DateTimeOffset followedAt) : InternalCommandBase(id)
{
    public ChatterId FollowerId { get; } = followerId;
    public ChatterId ChatterId { get; } = chatterId;
    public DateTimeOffset FollowedAt { get; } = followedAt;
}
