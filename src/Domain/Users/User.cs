using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Domain.Users.Rules;

namespace SocialMediaBackend.Domain.Users;

public class User : AuditableEntity<Guid>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();

    private User(string username, string nickname, DateOnly dateOfBirth, Media profilePicture)
    {
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture;

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

    public static async Task<User> CreateAsync(string username, string nickname, DateOnly dateOfBirth, 
        IUserExistsChecker userExistsChecker,
        Media? profilePicture = null)
    {
        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username));

        var pfp = profilePicture ?? Media.DefaultProfilePicture;

        return new User(username, nickname, dateOfBirth, pfp);
    }

    public Post? AddPost(string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        var post = Post.Create(Id, text, mediaItems as List<Media> ?? mediaItems?.ToList());
        if (post is null)
            return null;

        _posts.Add(post);
        return post;
    }

    public bool ChangeUsername(string username)
    {
        Username = username;
        return true;
    }

    public bool ChangeNickname(string nickname)
    {
        Nickname = nickname;
        return true;
    }
}
