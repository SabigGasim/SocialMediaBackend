namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class DomainException : Exception
{
    public DomainException(string message, string details = "") : base(message)
    {
        Details = details;
    }

    public string Details { get; }
}
