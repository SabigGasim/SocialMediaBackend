using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public record CreateCommentRequest([FromRoute]Guid PostId, [FromBody]string Text);
