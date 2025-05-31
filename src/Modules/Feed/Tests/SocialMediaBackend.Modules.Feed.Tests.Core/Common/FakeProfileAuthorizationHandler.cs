using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common;
public class FakeProfileAuthorizationHandler(FakeDbContext context) : ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>(context);