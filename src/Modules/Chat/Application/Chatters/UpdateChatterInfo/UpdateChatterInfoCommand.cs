using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.UpdateChatterInfo;

public class UpdateChatterInfoCommand : InternalCommandBase
{
    [JsonConstructor]
    public UpdateChatterInfoCommand(
        Guid id,
        Guid chatterId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic) : base(id)
    {
        ChatterId = chatterId;
        Username = username;
        Nickname = nickname;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
    }

    public Guid ChatterId { get; }
    public string Username { get; }
    public string Nickname { get; }
    public Media ProfilePicture { get; }
    public bool ProfileIsPublic { get; }
}
