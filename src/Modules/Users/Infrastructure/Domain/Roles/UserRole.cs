using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.Modules.Users.Domain.Users;
using DomainAuth = SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Roles;

public class UserRole
{
    public DomainAuth.Roles RoleId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    
    public Role Role { get; private set; } = default!;
    public User User { get; private set; } = default!;

    private UserRole() { }
    public UserRole(DomainAuth.Roles roleId, UserId userId)
    {
        this.RoleId = roleId;
        this.UserId = userId;
    }
}
