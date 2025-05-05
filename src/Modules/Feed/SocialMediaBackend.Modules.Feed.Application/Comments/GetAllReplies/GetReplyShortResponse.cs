using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

public record GetReplyShortResponse(
    Guid ReplyId,
    string Text,
    int LikesCount,
    int RepliesCount,
    GetAuthorResponse Author
    );
