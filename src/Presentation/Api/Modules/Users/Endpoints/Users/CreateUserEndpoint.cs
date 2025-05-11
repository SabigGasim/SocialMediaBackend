using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpPost(ApiEndpoints.Users.Create), AllowAnonymous]
public class CreateUserEndpoint(IUsersModule module) : RequestEndpoint<CreateUserRequest, CreateUserResponse>(module)
{
    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(
            req.Username,
            req.Nickname,
            req.DateOfBirth,
            req.ProfilePicture);

        await HandleCommandAsync(command, ct);
    }
}



public class CreateUserEndpointSummary : Summary<CreateUserEndpoint>
{
    public CreateUserEndpointSummary()
    {
        Summary = "Creates a new user in the system";
        Description = "Creates a new user in the system";
        Response<CreateUserResponse>(201, "User was successfully created");
        Response<ValidationFailureResponse>(400, "The request didn't pass validation checks");
        Response<ValidationFailureResponse>(400, "A user with this username already exists");
    }
}