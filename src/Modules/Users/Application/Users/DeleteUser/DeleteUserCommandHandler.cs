using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

public class DeleteUserCommandHandler(ApplicationDbContext context) : ICommandHandler<DeleteUserCommand>
{
    private readonly ApplicationDbContext _context = context;

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

        _context.Remove(user);
        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
