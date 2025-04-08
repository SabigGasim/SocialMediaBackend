using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public class UpdateUsernameCommand(Guid userId, string username) : CommandBase
{
    public Guid UserId { get; } = userId;
    public string Username { get; } = username;
}
