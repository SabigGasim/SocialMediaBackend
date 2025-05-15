using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.UnfollowAuthor;

[method: JsonConstructor]
public class UnfollowAuthorCommand(
    Guid id,
    AuthorId followerId,
    AuthorId authorId) : InternalCommandBase(id)
{
    public AuthorId FollowerId { get; } = followerId;
    public AuthorId AuthorId { get; } = authorId;
}
