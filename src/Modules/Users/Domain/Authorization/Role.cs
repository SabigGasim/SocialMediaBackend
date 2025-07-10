using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Domain.Authorization;

public enum Roles
{
    User, AdminUser, AppBasicPlan, AppPlusPlan
}

public sealed class Role
{
    private readonly List<User> _users = [];
    private readonly List<Permission> _permissions = [];

    public static readonly Role User = new Role(Roles.User);
    public static readonly Role AdminUser = new Role(Roles.AdminUser);
    public static readonly Role BasicPlan = new Role(Roles.AppBasicPlan);
    public static readonly Role PlusPlan = new Role(Roles.AppPlusPlan);

    public Roles Id { get; private set; }
    public string Name { get; private set; } = default!;
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

    private Role() { }

    public Role(Roles roleId)
    {
        Id = roleId;
        Name = $"{roleId}Role";
    }
}
