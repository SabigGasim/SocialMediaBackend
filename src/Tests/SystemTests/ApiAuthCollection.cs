namespace SocialMediaBackend.Tests.SystemTests;

[CollectionDefinition("Api & Auth")]
public class ApiAuthCollection : ICollectionFixture<App>, ICollectionFixture<AuthFixture>;
