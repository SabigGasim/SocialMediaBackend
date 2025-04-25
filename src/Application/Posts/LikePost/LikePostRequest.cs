using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Posts.LikePost;

public record LikePostRequest([FromRoute]Guid PostId);
