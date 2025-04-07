using Refit;
using SocialMediaBackend.Application.Users.CreateUser;

namespace SocialMediaBackend.Sdk;


public interface ISocialMediaApi
{
    [Post(ApiEndpoints.Users.Create)]
    public Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
}
