using SocialMediaBackend.Modules.Users.Application.Users.GetUser;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetComment;

public record GetCommentResponse(Guid CommentId, Guid PostId, string Text, int LikesCount, int RepliesCount, GetUserResponse User);
