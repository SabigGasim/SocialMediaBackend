using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.UnlikeComment;

public record UnlikeCommentRequest([FromRoute]Guid CommentId);
