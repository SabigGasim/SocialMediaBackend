using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;

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
