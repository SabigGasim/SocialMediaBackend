using SocialMediaBackend.Modules.Feed.Tests.Core.Common;

namespace SocialMediaBackend.Modules.Feed.Tests.UnitTests;

[CollectionDefinition("Api & Auth")]
public class ApiAuthCollection : ICollectionFixture<App>, ICollectionFixture<AuthFixture>;
