using System.Text.RegularExpressions;

namespace SocialMediaBackend.Modules.Users.Application.Users;

internal static partial class UserRegularExpressions
{
    [GeneratedRegex("^[a-zA-Z0-9_ ]{3,15}$", RegexOptions.Compiled)]
    internal static partial Regex NicknameRegex();

    [GeneratedRegex("^[a-zA-Z0-9_]{3,12}$", RegexOptions.Compiled)]
    internal static partial Regex UsernameRegex();
}
