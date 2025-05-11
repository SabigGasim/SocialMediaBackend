using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.CreateAuthor;

public class CreateAuthorCommand : InternalCommandBase
{
    [JsonConstructor]
    public CreateAuthorCommand(
        Guid id, //InternalCommandId
        AuthorId authorId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount) : base(id)
    {
        AuthorId = authorId;
        Username = username;
        Nickname = nickname;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
        FollowersCount = followersCount;
        FollowingCount = followingCount;
    }

    public AuthorId AuthorId { get; }
    public string Username { get; }
    public string Nickname { get; }
    public Media ProfilePicture { get; }
    public bool ProfileIsPublic { get; }
    public int FollowersCount { get; }
    public int FollowingCount { get; }
}
