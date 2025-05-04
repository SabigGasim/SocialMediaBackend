using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.DeleteComment;

public record DeleteCommentRequest([FromRoute]Guid CommentId, [FromRoute]Guid PostId);
