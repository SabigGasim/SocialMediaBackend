using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles;

public class AuthorRole
{
    public Feed.Domain.Authorization.Roles RoleId { get; private set; } = default!;
    public AuthorId AuthorId { get; private set; } = default!;
    
    public Role Role { get; private set; } = default!;
    public Author Author { get; private set; } = default!;

    private AuthorRole() { }
    public AuthorRole(Feed.Domain.Authorization.Roles roleId, AuthorId authorId)
    {
        RoleId = roleId;
        AuthorId = authorId;
    }
}
