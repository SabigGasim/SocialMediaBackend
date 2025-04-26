using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;

namespace Tests.Core.Common;

public class FakeUserResource : Entity<FakeUserResourceId>, IUserResource
{
    public User User { get; set; } = null!;
    public UserId UserId { get; set; } = null!;

    public static FakeUserResource Create(User user)
    {
        return new FakeUserResource()
        {
            Id = new(Guid.NewGuid()),
            User = user,
            UserId = user.Id,
        };
    }
}
