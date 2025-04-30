using Tests.Core.Common;

namespace SocialMediaBackend.UnitTests;

[CollectionDefinition("Api & Auth")]
public class ApiAuthCollection : ICollectionFixture<App>, ICollectionFixture<AuthFixture>;
