using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;

public record LikePostRequest([FromRoute] Guid PostId);
