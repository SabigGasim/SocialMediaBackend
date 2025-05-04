using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;
public class FakeProfileAuthorizationHandler(FakeDbContext context) : ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>(context);