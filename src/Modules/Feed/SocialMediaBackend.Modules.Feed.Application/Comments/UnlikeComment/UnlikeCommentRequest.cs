using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;

public record UnlikeCommentRequest([FromRoute] Guid CommentId);
