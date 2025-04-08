using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
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
            return HandlerResponse.CreateError("User was not found.", command.UserId);
        }

        _dbContext.Remove(user);
        var deleted = await _dbContext.SaveChangesAsync() > 0;
        return deleted
            ? HandlerResponse.CreateSuccess()
            : HandlerResponse.CreateError("An error occured deleting the user.", command.UserId);
    }
}
