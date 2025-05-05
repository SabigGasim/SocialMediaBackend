using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;

public record UnlikePostRequest([FromRoute] Guid PostId);
