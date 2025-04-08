using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public record UpdateNicknameRequest([FromRoute]Guid UserId, [FromBody]string Nickname);
