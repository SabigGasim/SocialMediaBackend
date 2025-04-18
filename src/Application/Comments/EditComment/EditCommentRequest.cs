namespace SocialMediaBackend.Application.Comments.EditComment;

public record EditCommentRequest(Guid CommentId, string Text);
