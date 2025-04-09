using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteUserCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HandlerResponse> ExecuteAsync(DeleteUserCommand command, CancellationToken ct)
    {
        var user = await _dbContext.Users.FindAsync(command.UserId, ct);
        if(user is null)
        {
            return ("User was not found", HandlerResponseStatus.NotFound, command.UserId);
        }

        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();

        return HandlerResponseStatus.Deleted;
    }
}
