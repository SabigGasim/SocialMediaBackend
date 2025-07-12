using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(SubscriptionsDbContext context) : ICommandHandler<CreateUserCommand>
{
    private readonly SubscriptionsDbContext _context = context;

    public Task<HandlerResponse> ExecuteAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = new User(command.UserId);

        _context.Add(user);
        _context.Set<UserRole>().Add(new UserRole(Roles.User, user.Id));

        return Task.FromResult((HandlerResponse)HandlerResponseStatus.NoContent);
    }
}
