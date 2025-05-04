using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

public class UpdateNicknameHandler : ICommandHandler<UpdateNicknameCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateNicknameHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateNicknameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([command.UserId], ct);
        if (user is null)
        {
            return ("No user exists with the given Id", HandlerResponseStatus.NotFound, command.UserId);
        }

        var nicknameIsModified = user.ChangeNickname(command.Nickname);
        if(!nicknameIsModified)
        {
            return ("Nickname was not modified", HandlerResponseStatus.BadRequest, command.Nickname);
        }

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
