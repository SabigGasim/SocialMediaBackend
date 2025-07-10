using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Authorization;

public enum Roles
{
    Chatter, AdminChatter, AppBasicPlan, AppPlusPlan
}

public sealed class Role
{
    private readonly List<Chatter> _chatters = [];
    private readonly List<Permission> _permissions = [];

    public static readonly Role Chatter = new Role(Roles.Chatter);
    public static readonly Role AdminChatter = new Role(Roles.AdminChatter);
    public static readonly Role BasicPlan = new Role(Roles.AppBasicPlan);
    public static readonly Role PlusPlan = new Role(Roles.AppPlusPlan);

    public Roles Id { get; private set; }
    public string Name { get; private set; } = default!;
    public IReadOnlyCollection<Chatter> Chatters => _chatters.AsReadOnly();
    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

    private Role() { }
    public Role(Roles roleId)
    {
        Id = roleId;
        Name = $"{roleId}Role";
    }
}
