namespace SocialMediaBackend.Application.Common;

public static class DateExtensions
{
    public static DateOnly ToDateOnly(this DateTime date)
    {
        return new DateOnly(date.Year, date.Month, date.Day);
    }

    public static DateOnly ToDateOnly(this DateTimeOffset date)
    {
        return new DateOnly(date.Year, date.Month, date.Day);
    }
}
