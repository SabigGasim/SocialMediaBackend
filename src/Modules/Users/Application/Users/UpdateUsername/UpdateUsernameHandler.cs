using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUsername;

public class UpdateUsernameHandler : ICommandHandler<UpdateUsernameCommand>
{
    private readonly UsersDbContext _context;
    private readonly IUserExistsChecker _userExistsChecker;

    public UpdateUsernameHandler(UsersDbContext context, IUserExistsChecker userExistsChecker)
    {
        _context = context;
        _userExistsChecker = userExistsChecker;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateUsernameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([new UserId(command.UserId)], ct);
        if (user is null)
        {
            return ("No user exists with the given Id", HandlerResponseStatus.NotFound, command.UserId);
        }

        var result = await user.ChangeUsernameAsync(command.Username, _userExistsChecker, ct);
        if(!result.IsSuccess)
        {
            return result;
        }

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
