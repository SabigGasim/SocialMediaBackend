using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Users.UpdateUsername;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public class UpdateNicknameHandler : ICommandHandler<UpdateNicknameCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateNicknameHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateNicknameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(command.UserId);
        if (user is null)
        {
            return HandlerResponse.CreateError("Bad request: no user exists with the given Id", command.UserId);
        }

        if (user.Nickname == command.Nickname)
        {
            return HandlerResponse.CreateError($"Bad request: nickname is already {command.Nickname}");
        }

        user.ChangeNickname(command.Nickname);
        await _context.SaveChangesAsync();

        return HandlerResponse.CreateSuccess();
    }
}
