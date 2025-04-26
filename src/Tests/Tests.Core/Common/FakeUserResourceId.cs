using SocialMediaBackend.Domain.Common;

namespace Tests.Core.Common;
public record FakeUserResourceId(Guid Id) : TypedIdValueBase<Guid>(Id);
