using SocialMediaBackend.Application.Users.GetUser;

namespace SocialMediaBackend.Application.Comments.GetComment;

public record GetCommentResponse(Guid CommentId, Guid PostId, string Text, int LikesCount, int RepliesCount, GetUserResponse User);
