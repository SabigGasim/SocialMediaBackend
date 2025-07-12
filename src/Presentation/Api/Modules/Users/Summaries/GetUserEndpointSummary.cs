using FastEndpoints;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Api.Modules.Users.Endpoints;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

internal class GetUserEndpointSummary : Summary<GetUserEndpoint>
{
    public GetUserEndpointSummary()
    {
        Summary = "Gets a user with the specified id or username from the system";
        Description = "Gets a user with the specified id or username from the system";
        Response<GetUserResponse>(200, "A user with the given id or username was returned");
        Response<ValidationFailureResponse>(400, "The given request parameter isn't valid for a username or an id");
        Response(404, "No user with the given id or username was found");
    }
}
