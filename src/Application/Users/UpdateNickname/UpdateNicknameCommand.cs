using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public class UpdateNicknameCommand(Guid userId, string nickname) : CommandBase
{
    public Guid UserId { get; } = userId;
    public string Nickname { get; } = nickname;
}
