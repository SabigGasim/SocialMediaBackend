using Tests.Core.Common;

namespace SocialMediaBackend.Modules.Users.UnitTests;

[CollectionDefinition("Api & Auth")]
public class ApiAuthCollection : ICollectionFixture<App>, ICollectionFixture<AuthFixture>;
