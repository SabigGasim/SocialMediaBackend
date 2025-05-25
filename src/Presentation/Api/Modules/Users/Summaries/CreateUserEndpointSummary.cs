using FastEndpoints;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

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