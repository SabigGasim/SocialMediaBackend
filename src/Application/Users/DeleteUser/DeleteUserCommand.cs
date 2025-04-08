using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.DeleteUser;

public class DeleteUserCommand(Guid userId) : CommandBase
{
    public Guid UserId { get; } = userId;
}
