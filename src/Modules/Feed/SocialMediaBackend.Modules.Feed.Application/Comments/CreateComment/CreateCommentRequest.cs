using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

public record CreateCommentRequest([FromRoute] Guid PostId, [FromBody] string Text);
