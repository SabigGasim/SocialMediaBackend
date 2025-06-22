namespace SocialMediaBackend.Modules.Payments.Contracts;

public record class ProductReference(string Type, string Id)
{
    public string Value => $"{Type}:{Id}";
}
