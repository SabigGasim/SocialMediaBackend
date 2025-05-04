using SocialMediaBackend.Modules.Users.Application.Auth;
using Tests.Core.Common.Users;

namespace Tests.Core.Common;
public class FakeProfileAuthorizationHandler(FakeDbContext context) : ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>(context);