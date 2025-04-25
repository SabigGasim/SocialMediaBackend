using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Posts.UnlikePost;

public record UnlikePostRequest([FromRoute]Guid PostId);
