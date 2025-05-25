using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

public class UpdateNicknameHandler(UsersDbContext context) : ICommandHandler<UpdateNicknameCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UpdateNicknameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([new UserId(command.UserId)], ct);

        var result = user!.ChangeNickname(command.Nickname);
        if(!result.IsSuccess)
        {
            return result;
        }

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
