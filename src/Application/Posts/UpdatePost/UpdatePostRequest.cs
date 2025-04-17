using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public record UpdatePostRequest([FromRoute]Guid PostId, [FromBody]string Text);
