using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.UpdateAuthorInfo;

public class UpdateAuthorInfoCommand : InternalCommandBase
{
    [JsonConstructor]
    public UpdateAuthorInfoCommand(
        Guid id,
        Guid authorId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic) : base(id)
    {
        AuthorId = authorId;
        Username = username;
        Nickname = nickname;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
    }

    public Guid AuthorId { get; }
    public string Username { get; }
    public string Nickname { get; }
    public Media ProfilePicture { get; }
    public bool ProfileIsPublic { get; }
}
