using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.LikeComment;

public record LikeCommentRequest([FromRoute]Guid CommentId);
