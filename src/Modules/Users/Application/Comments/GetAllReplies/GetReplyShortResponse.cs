using SocialMediaBackend.Modules.Users.Application.Users.GetUser;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;

public record GetReplyShortResponse(
    Guid ReplyId,
    string Text,
    int LikesCount,
    int RepliesCount,
    GetUserResponse user
    );
