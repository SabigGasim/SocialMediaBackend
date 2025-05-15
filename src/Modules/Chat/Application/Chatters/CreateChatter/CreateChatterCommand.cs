using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.CreateChatter;

[method: JsonConstructor]
public class CreateChatterCommand(
    Guid id, //InternalCommandId
    ChatterId chatterId,
    string username,
    string nickname,
    Media profilePicture,
    bool profileIsPublic,
    int followersCount,
    int followingCount) : InternalCommandBase(id)
{
    public ChatterId ChatterId { get; } = chatterId;
    public string Username { get; } = username;
    public string Nickname { get; } = nickname;
    public Media ProfilePicture { get; } = profilePicture;
    public bool ProfileIsPublic { get; } = profileIsPublic;
    public int FollowersCount { get; } = followersCount;
    public int FollowingCount { get; } = followingCount;
}
