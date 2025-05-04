using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;

public record CreateCommentRequest([FromRoute]Guid PostId, [FromBody]string Text);
