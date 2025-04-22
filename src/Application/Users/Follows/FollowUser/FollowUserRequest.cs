using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Users.Follows.FollowUser;

public record FollowUserRequest(Guid UserId);
