using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Core.Common.Users;

[Table("FakeUserResource")]
public class FakeUserResource : Entity<FakeUserResourceId>, IUserResource
{
    public User User { get; set; } = null!;
    public UserId UserId { get; set; } = null!;

    public static FakeUserResource Create(User user)
    {
        return new FakeUserResource()
        {
            Id = FakeUserResourceId.New(),
            User = user,
            UserId = user.Id,
        };
    }
}
