using SocialMediaBackend.Modules.Users.Tests.Core.Common;

namespace SocialMediaBackend.Modules.Users.Tests.IntegrationTests;

[CollectionDefinition("Api & Auth")]
public class ApiAuthCollection : ICollectionFixture<App>, ICollectionFixture<AuthFixture>;
