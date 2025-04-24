using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public class UpdateUsernameCommand(Guid userId, string username) : CommandBase
{
    public UserId UserId { get; } = new(userId);
    public string Username { get; } = username;
}
