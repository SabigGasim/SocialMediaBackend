using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

public class DeleteUserCommandHandler(UsersDbContext context) : ICommandHandler<DeleteUserCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteUserCommand command, CancellationToken ct)
    {
        if(!command.IsAdmin && new UserId(command.UserId) != command.UserToDeleteId)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized, command.UserToDeleteId);
        }

        var user = await _context.Users.FindAsync([command.UserToDeleteId], ct);
        if(user is null)
        {
            return ("User was not found", HandlerResponseStatus.NotFound, command.UserId);
        }

        user.Delete();

        _context.Remove(user);

        return HandlerResponseStatus.Deleted;
    }
}
