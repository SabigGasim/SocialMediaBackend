using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetComment;

public record GetCommentRequest([FromRoute]Guid CommentId);
