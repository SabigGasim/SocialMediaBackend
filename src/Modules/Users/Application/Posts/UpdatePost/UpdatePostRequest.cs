using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Posts.UpdatePost;

public record UpdatePostRequest([FromRoute]Guid PostId, [FromBody]string Text);
