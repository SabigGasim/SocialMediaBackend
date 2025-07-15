namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract record ValueObject
{
    protected abstract IEnumerable<object> GetComponents();
}
