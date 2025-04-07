using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Domain.Users;

public class User : AuditableEntity<Guid>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();

    private User(string username, string nickname,
        DateOnly dateOfBirth, Media? profilePicture = null)
    {
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture ?? Media.DefaultProfilePicture;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";
    }

    private User() { }

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public DateOnly DateOfBirth { get; private set; }
    public Media ProfilePicture { get; private set; } = default!;

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    public static User Create(string username, string nickname, DateOnly dateOfBirth, Media? profilePicture = null)
    {
        //TODO
        //Rules for username, nickname, date of birth, pfp

        return new User(username, nickname, dateOfBirth, profilePicture);
    }

    public Post? AddPost(string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        var post = Post.Create(Id, text, mediaItems as List<Media> ?? mediaItems?.ToList());
        if (post is null)
            return null;

        _posts.Add(post);
        return post;
    }
}
