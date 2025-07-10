using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Domain.Authorization;

public enum Roles
{
    Author, AdminAuthor, AppBasicPlan, AppPlusPlan
}

public sealed class Role
{
    private readonly List<Author> _authors = [];
    private readonly List<Permission> _permissions = [];

    public static readonly Role Author = new Role(Roles.Author);
    public static readonly Role AdminAuthor = new Role(Roles.AdminAuthor);
    public static readonly Role BasicPlan = new Role(Roles.AppBasicPlan);
    public static readonly Role PlusPlan = new Role(Roles.AppPlusPlan);

    public Roles Id { get; private set; }
    public string Name { get; private set; } = default!;
    public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();
    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

    private Role() { }
    public Role(Roles roleId)
    {
        Id = roleId;
        Name = $"{roleId}Role";
    }
}
