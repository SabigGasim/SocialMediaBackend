using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserExistsChecker _userExistsChecker;
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(IUserExistsChecker userExistsChecker, ApplicationDbContext context)
    {
        _userExistsChecker = userExistsChecker;
        _context = context;
    }

    public async Task<HandlerResponse<CreateUserResponse>> ExecuteAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await User.CreateAsync(command.Username, command.Nickname, command.DateOfBirth,
            _userExistsChecker, command.ProfilePicture, ct);

        if(user is null)
        {
            return ("User was not created", HandlerResponseStatus.BadRequest, command);
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        return (user.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
