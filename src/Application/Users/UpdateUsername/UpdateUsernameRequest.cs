using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public record UpdateUsernameRequest([FromRoute]Guid UserId, [FromBody]string Username);
