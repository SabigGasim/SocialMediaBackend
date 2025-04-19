using SocialMediaBackend.Application.Users.GetUser;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public record GetReplyShortResponse(
    Guid ReplyId,
    string Text,
    int LikesCount,
    int RepliesCount,
    GetUserResponse user
    );
