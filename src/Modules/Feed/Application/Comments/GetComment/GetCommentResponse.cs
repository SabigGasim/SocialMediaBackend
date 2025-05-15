using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

public record GetCommentResponse(
    Guid CommentId,
    Guid PostId, 
    string Text,
    int LikesCount, 
    int RepliesCount, 
    GetAuthorResponse Author);
