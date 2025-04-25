using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public record ReplyToCommentRequest([FromRoute]Guid ParentId, [FromBody]string Text);
