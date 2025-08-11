using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

[HasPermission(Permissions.GetFullUserDetails)]
public sealed class GetFullUserDetailsQuery : QueryBase<GetFullUserDetailsResponse>, IRequireAuthorization;