using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

public class FakeUserResource : Entity<FakeUserResourceId>, IUserResource
{
    private FakeUserResource() { }
    public Author Author { get; private set; } = default!;
    public AuthorId AuthorId { get; private set; } = default!;

    public static FakeUserResource Create(Author user)
    {
        return new FakeUserResource()
        {
            Id = FakeUserResourceId.New(),
            Author = user,
            AuthorId = user.Id,
        };
    }
}
