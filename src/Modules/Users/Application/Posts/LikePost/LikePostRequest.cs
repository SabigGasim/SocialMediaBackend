using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Posts.LikePost;

public record LikePostRequest([FromRoute]Guid PostId);
