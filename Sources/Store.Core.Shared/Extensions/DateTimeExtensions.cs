namespace Store.Core.Shared;

public static class DateTimeExtensions
{
    public static string ToDateTimeString(this DateTime dateTime)
        => dateTime.ToString("dd MMM yyyy, HH:mm");
}