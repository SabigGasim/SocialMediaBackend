using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public record DeleteCommentRequest([FromRoute]Guid CommentId, [FromRoute]Guid PostId);
