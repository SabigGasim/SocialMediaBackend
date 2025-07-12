using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using DomainAuth = SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles;

public class UserRole
{
    public DomainAuth.Roles RoleId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    
    public Role Role { get; private set; } = default!;
    public User User { get; private set; } = default!;

    private UserRole() { }
    public UserRole(DomainAuth.Roles roleId, UserId userId)
    {
        RoleId = roleId;
        UserId = userId;
    }
}
