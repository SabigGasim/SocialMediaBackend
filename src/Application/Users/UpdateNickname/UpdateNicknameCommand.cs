using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public class UpdateNicknameCommand(Guid userId, string nickname) : CommandBase
{
    public UserId UserId { get; } = new(userId);
    public string Nickname { get; } = nickname;
}
