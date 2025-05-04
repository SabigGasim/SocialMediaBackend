using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Posts.UnlikePost;

public record UnlikePostRequest([FromRoute]Guid PostId);
