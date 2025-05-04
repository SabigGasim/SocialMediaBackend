using System.Text;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;
public static class TextHelper
{
    public static string CreateRandom(int length)
    {
        var builder = new StringBuilder(Guid.NewGuid().ToString().Replace("-", ""));

        for (int i = 1; i * 32 < length; i++)
        {
            builder.Append(Guid.NewGuid().ToString().Replace("-", ""));
        }

        return builder.ToString()[..length];
    }
}
