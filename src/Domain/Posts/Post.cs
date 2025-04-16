using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Posts;

public class Post : AuditableEntity<Guid>
{
    private readonly List<PostLike> _likes = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Media> _mediaItems;

    private Post(Guid userId, string? text,
        List<Media>? mediaItems = null)
    {
        UserId = userId;
        Text = text;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";

        _mediaItems = new(mediaItems ?? []);
    }

    private Post() => _mediaItems = new();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public int LikesCount { get; private set; }
    public int CommentsCount { get; private set; }
    public string? Text { get; private set; }

    public IReadOnlyCollection<PostLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Media> MediaItems => _mediaItems.AsReadOnly();

    public static Post? Create(Guid userId, string? text = null, List<Media>? mediaItems = null)
    {
        if (string.IsNullOrEmpty(text) && mediaItems == null)
        {
            return null;
        }

        return new Post(userId, text, mediaItems);
    }

    public bool UpdatePost(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = this.Id.ToString();
        
        return true;
    }

    public bool AddComment(string text, Guid userId)
    {
        var comment = Comment.Create(userId, Id, text, null);
        if (comment is null)
            return false;

        _comments.Add(comment);
        CommentsCount++;

        return true;
    }

    public async Task<bool> ReplyToCommentAsync(
        string text,
        Guid userId,
        Guid parentCommentId,
        ICommentLookupService commentLookupService)
    {
        var parentComment = await GetComment(parentCommentId, commentLookupService);

        return parentComment is not null
            ? parentComment.AddReply(this.Id, userId, text)
            : true;
    }

    public async Task<bool> RemoveCommentAsync(
        Guid commentId,
        ICommentLookupService commentLookupService)
    {
        var comment = await GetCommentWithParent(commentId, commentLookupService);
        if (comment is null)
            return false;

        _comments.Remove(comment);
        
        return comment.ParentComment is not null
            ? comment.ParentComment.RemoveReply(comment)
            : true;
    }

    public bool AddLike(Guid userId)
    {
        if (_likes.Any(l => l.UserId == userId))
            return false;

        var postLike = PostLike.Create(userId, Id);
        _likes.Add(postLike);
        LikesCount++;

        return true;
    }

    public bool RemoveLike(Guid userId)
    {
        var postLike = _likes.Find(l => l.UserId == userId);

        if (postLike is null)
            return false;

        _likes.Remove(postLike);
        LikesCount--;

        return true;
    }

    public async Task<bool> LikeCommentAsync(
        Guid userId, 
        Guid commentId,
        ICommentLookupService commentLookupService)
    {
        var comment = await GetComment(commentId, commentLookupService);

        return comment is not null
            ? comment.AddLike(userId)
            : false;
    }

    public async Task<bool> UnlikeCommentAsync(
        Guid userId, 
        Guid commentId,
        ICommentLookupService commentLookupService)
    {
        var comment = await GetCommentAndLikeByUser(commentId, userId, commentLookupService);

        return comment is not null
            ? comment.RemoveLike(userId)
            : false;
    }

    private async Task<Comment?> GetComment(Guid commentId, ICommentLookupService commentLookupService)
    {
        return _comments.Find(x => x.Id == commentId)
            ?? await commentLookupService.FindAsync(commentId);
    }

    private async Task<Comment?> GetCommentAndLikeByUser(
        Guid commentId, 
        Guid userId,
        ICommentLookupService commentLookupService)
    {
        return _comments.Find(x => x.Id == commentId)
            ?? await commentLookupService.FindCommentLikedByUser(this.Id, userId);
    }

    private async Task<Comment?> GetCommentWithParent(Guid commentId, ICommentLookupService commentLookupService)
    {
        return _comments.Find(x => x.Id == commentId)
            ?? await commentLookupService.FindAsync(commentId, includeParent: true);
    }
}
