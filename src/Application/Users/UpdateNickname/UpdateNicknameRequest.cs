using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public record UpdateNicknameRequest([FromBody]string Nickname);
