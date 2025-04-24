using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Common;

public interface IUserResource
{
    UserId UserId { get; }
    User User { get; }
}
