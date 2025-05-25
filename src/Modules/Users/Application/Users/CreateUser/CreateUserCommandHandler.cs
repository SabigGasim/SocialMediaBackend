using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserExistsChecker _userExistsChecker;
    private readonly UsersDbContext _context;

    public CreateUserCommandHandler(
        IUserExistsChecker userExistsChecker,
        UsersDbContext context)
    {
        _userExistsChecker = userExistsChecker;
        _context = context;
    }

    public async Task<HandlerResponse<CreateUserResponse>> ExecuteAsync(CreateUserCommand command, CancellationToken ct)
    {
        var result = await User.CreateAsync(
            command.Username, 
            command.Nickname, 
            command.DateOfBirth,
            _userExistsChecker, 
            command.ProfilePicture, ct);

        if (!result.IsSuccess)
        {
            return result;
        }

        var user = result.Payload;

        _context.Users.Add(user);

        return (user.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
