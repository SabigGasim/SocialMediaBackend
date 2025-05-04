using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Domain.Common;

public interface IUserResource
{
    UserId UserId { get; }
    User User { get; }
}
