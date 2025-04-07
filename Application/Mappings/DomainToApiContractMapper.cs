using SocialMediaBackend.Application.Users.CreateUser;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static CreateUserResponse MapToResponse(this User user)
    {
        return new CreateUserResponse(
            user.Id,
            user.Username,
            user.Nickname,
            user.DateOfBirth,
            user.ProfilePicture);
    }
}
