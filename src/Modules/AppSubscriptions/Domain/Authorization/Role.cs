using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

public enum Roles
{
    User, AdminUser
}

public sealed class Role
{
    private readonly List<User> _users = [];
    private readonly List<Permission> _permissions = [];

    public static readonly Role User = new Role(Roles.User);
    public static readonly Role AdminUser = new Role(Roles.AdminUser);

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