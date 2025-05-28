using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Domain.Authors;

public class Author : AggregateRoot<AuthorId>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Follow> _followers = new();
    private readonly List<Follow> _followings = new();

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
    public IReadOnlyCollection<Follow> Followers => _followers.AsReadOnly();
    public IReadOnlyCollection<Follow> Followings => _followings.AsReadOnly();

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

    public Result<Post> CreatePost(string text, IEnumerable<Media>? mediaItems = null)
    {
        var result = Post.Create(Id, text, mediaItems as List<Media> ?? mediaItems?.ToList());
        if (result.IsSuccess)
        {
            _posts.Add(result.Payload);
        }

        return result;
    }

    public Result RemovePost(PostId postId)
    {
        var post = _posts.Find(x => x.Id == postId)!;
        if (post is null)
        {
            return Result.Failure(FailureCode.NotFound, "Post");
        }

        _posts.Remove(post);

        return Result.Success();
    }
}
