using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.Follows.UnfollowChatter;

[method: JsonConstructor]
public class UnfollowChatterCommand(
    Guid id,
    ChatterId followerId,
    ChatterId chatterId) : InternalCommandBase(id)
{
    public ChatterId FollowerId { get; } = followerId;
    public ChatterId ChatterId { get; } = chatterId;
}
