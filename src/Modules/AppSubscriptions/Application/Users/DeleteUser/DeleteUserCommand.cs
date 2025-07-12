using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.DeleteUser;

[method: JsonConstructor]
public sealed class DeleteUserCommand(Guid id, UserId userId) : InternalCommandBase(id)
{
    public UserId UserId { get; } = userId;
}
