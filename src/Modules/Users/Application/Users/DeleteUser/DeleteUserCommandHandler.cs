using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    UsersDbContext context,
    IUserContext userContext,
    IPermissionManager permissionManager)
    : ICommandHandler<DeleteUserCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IUserContext _userContext = userContext;
    private readonly IPermissionManager _permissionManager = permissionManager;

    public async Task<HandlerResponse> ExecuteAsync(DeleteUserCommand command, CancellationToken ct)
    {
        if (!await CanDeleteUserAsync(command, ct))
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized, command.UserToDeleteId);
        }

        var user = await _context.Users.FindAsync([command.UserToDeleteId], ct);
        if (user is null)
        {
            return ("User was not found", HandlerResponseStatus.NotFound, _userContext.UserId.Value);
        }

        user.Delete();

        _context.Remove(user);

        return HandlerResponseStatus.Deleted;
    }

    private async Task<bool> CanDeleteUserAsync(DeleteUserCommand command, CancellationToken ct)
    {
        return _userContext.UserId == command.UserToDeleteId ||
               await _permissionManager.UserIsInRole(
                   _userContext.UserId.Value, 
                   (int)Roles.AdminUser, 
                   ct);
    }
}
