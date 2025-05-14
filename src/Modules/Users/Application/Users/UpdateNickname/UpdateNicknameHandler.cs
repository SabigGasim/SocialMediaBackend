using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

public class UpdateNicknameHandler : ICommandHandler<UpdateNicknameCommand>
{
    private readonly UsersDbContext _context;

    public UpdateNicknameHandler(UsersDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateNicknameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([new UserId(command.UserId)], ct);
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
