using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.FollowAuthor;

[method: JsonConstructor]
public class FollowAuthorCommand(
    Guid id,
    AuthorId followerId,
    AuthorId authorId,
    DateTimeOffset followedAt) : InternalCommandBase(id)
{
    public AuthorId FollowerId { get; } = followerId;
    public AuthorId AuthorId { get; } = authorId;
    public DateTimeOffset FollowedAt { get; } = followedAt;
}
