using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;
public record FakeUserResourceId(Guid Id) : TypedIdValueBase<Guid>(Id)
{
    public static FakeUserResourceId New() => new(Guid.NewGuid());
}
