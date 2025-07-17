namespace SocialMediaBackend.BuildingBlocks.Domain.Helpers;

public static class DateTimeHelper
{
    public static DateTimeOffset ToUtcDateTimeOffset(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime, TimeSpan.Zero);
    }
}
