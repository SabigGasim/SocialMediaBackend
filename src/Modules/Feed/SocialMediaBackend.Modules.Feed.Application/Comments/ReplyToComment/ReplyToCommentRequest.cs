using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;

public record ReplyToCommentRequest([FromRoute] Guid ParentId, [FromBody] string Text);
