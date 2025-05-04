using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUsername;

public record UpdateUsernameRequest([FromBody]string Username);
