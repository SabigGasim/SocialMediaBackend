namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

public record GetChatterShortResponse(
    Guid Id,
    string Username,
    string Nickname,
    string? ProfilePictureUrl = null);
