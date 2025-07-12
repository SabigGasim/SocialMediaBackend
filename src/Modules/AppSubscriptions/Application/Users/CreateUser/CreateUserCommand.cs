using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.CreateUser;

[method: JsonConstructor]
public class CreateUserCommand(Guid id, UserId userId) : InternalCommandBase(id)
{
    public UserId UserId { get; } = userId;
}
