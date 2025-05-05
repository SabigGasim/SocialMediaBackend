using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UpdatePost;

public record UpdatePostRequest([FromRoute] Guid PostId, [FromBody] string Text);
