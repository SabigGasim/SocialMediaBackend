using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Domain.Authors;

public class Author : AggregateRoot<AuthorId>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();

    private Author() { }

    private Author(
        AuthorId authorId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount) : base()
    {
        Id = authorId;
        Username = username;
        Nickname = nickname;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
        FollowersCount = followersCount;
        FollowingCount = followingCount;
    }

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public Media ProfilePicture { get; private set; } = default!;
    public bool ProfileIsPublic { get; private set; }
    public int FollowersCount { get; private set; }
    public int FollowingCount { get; private set; }

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    public static Author Create(
        AuthorId authorId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount)
    {
        return new Author(authorId, username, nickname, profilePicture, profileIsPublic, followersCount, followingCount);
    }

    public Post? AddPost(string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        var post = Post.Create(Id, text, mediaItems as List<Media> ?? mediaItems?.ToList());
        if (post is null)
            return null;

        _posts.Add(post);
        return post;
    }

    public bool RemovePost(PostId postId)
    {
        var post = _posts.Find(x => x.Id == postId)!;
        return _posts.Remove(post);
    }
}
