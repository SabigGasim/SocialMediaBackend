using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;
public record FakeUserResourceId(Guid Id) : TypedIdValueBase<Guid>(Id)
{
    public static FakeUserResourceId New() => new(Guid.NewGuid());
}
