using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common.Comments;

public static class CommentFactory
{
    public static Comment Create(PostId? postId = null, AuthorId? userId = null, string text = "text")
    {
        return Comment.Create(postId ?? PostId.New(), userId ?? AuthorId.New(), text, null);
    }

    public static Comment CreateReply(Comment comment, string text = "text")
    {
        return Comment.Create(comment.PostId, comment.AuthorId, text, comment.Id);
    }
}
