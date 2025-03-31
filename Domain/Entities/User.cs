using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;

public class User : AuditableEntity<Guid>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public DateOnly DateOfBirth { get; private set; }
    public Media ProfilePicture { get; private set; } = default!;

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
}
