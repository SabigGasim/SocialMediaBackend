using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public record UpdateUsernameRequest([FromBody]string Username);
