using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.LikeComment;

public record LikeCommentRequest([FromRoute]Guid CommentId);
