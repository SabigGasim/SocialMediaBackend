using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.UnlikeComment;

public record UnlikeCommentRequest([FromRoute]Guid CommentId);
