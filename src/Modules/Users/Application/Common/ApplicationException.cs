namespace SocialMediaBackend.Modules.Users.Application.Common;

public abstract class ApplicationException : Exception
{
    public ApplicationException(string details) : base(details)
    {
        Details = details;
    }

    public string Details { get; }
}
