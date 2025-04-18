using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.GetComment;

public record GetCommentRequest([FromRoute]Guid CommentId);
