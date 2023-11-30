namespace Domain.Extensions;

public static class DateExtension
{
    public static string ToIsoDate(this DateTime dateTime)
    {
        // "2023-01-15"
        return dateTime.ToString("yyyy'-'MM'-'dd");
    }

    public static string? ToIsoDate(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToIsoDate() : null;
    }
    public static string ToIsoDateTime(this DateTime dateTime)
    {
        // "2023-01-15T10:00:00.000+00:00"
        return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'+00:00'");
    }

    public static string? ToIsoDateTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToIsoDateTime() : null;
    }
}
