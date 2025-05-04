using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.ReplyToComment;

public record ReplyToCommentRequest([FromRoute]Guid ParentId, [FromBody]string Text);
