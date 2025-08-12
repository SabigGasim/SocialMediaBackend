using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

internal sealed class UpdateNicknameCommandHandler(UsersDbContext context, IUserContext userContext) 
    : ICommandHandler<UpdateNicknameCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<HandlerResponse> ExecuteAsync(UpdateNicknameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([_userContext.UserId], ct);

        var result = user!.ChangeNickname(command.Nickname);
        if(!result.IsSuccess)
        {
            return result;
        }

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
