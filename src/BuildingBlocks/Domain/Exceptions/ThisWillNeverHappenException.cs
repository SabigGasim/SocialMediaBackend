namespace SocialMediaBackend.BuildingBlocks.Domain.Exceptions;

public class ThisWillNeverHappenException : Exception
{
    public ThisWillNeverHappenException() { }
    public ThisWillNeverHappenException(string message) : base(message) { }
}
