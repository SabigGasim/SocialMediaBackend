using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

public record DeleteCommentRequest([FromRoute] Guid CommentId, [FromRoute] Guid PostId);
