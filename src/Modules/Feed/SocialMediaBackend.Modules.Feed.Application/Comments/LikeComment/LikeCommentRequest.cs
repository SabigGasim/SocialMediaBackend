using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.LikeComment;

public record LikeCommentRequest([FromRoute] Guid CommentId);
