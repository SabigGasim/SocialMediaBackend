namespace SocialMediaBackend.Modules.Users.Domain.Common;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }
    void ClearDomainEvents();
}
