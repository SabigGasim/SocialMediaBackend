using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

public record GetCommentRequest([FromRoute] Guid CommentId);
