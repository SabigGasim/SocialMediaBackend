using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.DeleteUser;

public class DeleteUserCommand(Guid userId) : CommandBase
{
    public UserId UserId { get; } = new(userId);
}
