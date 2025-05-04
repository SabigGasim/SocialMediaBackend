using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

public record UpdateNicknameRequest([FromBody]string Nickname);
