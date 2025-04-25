using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.DeleteUser;

public class DeleteUserCommandHandler(ApplicationDbContext context) : ICommandHandler<DeleteUserCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteUserCommand command, CancellationToken ct)
    {
        if(!command.IsAdmin && command.UserId != command.UserToDeleteId)
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
