namespace March7thHoney.Util;

public static class ServerTimeProvider
{
    public static long GetServerUnixSec()
    {
        if (!ConfigManager.Config.ServerOption.ServerTime.EnableFakeServerTime)
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        return new DateTimeOffset(GetFixedLocalDate()).ToUnixTimeSeconds();
    }

    public static long GetServerUnixMs()
    {
        if (!ConfigManager.Config.ServerOption.ServerTime.EnableFakeServerTime)
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        return new DateTimeOffset(GetFixedLocalDate()).ToUnixTimeMilliseconds();
    }

    private static DateTime GetFixedLocalDate()
    {
        var fixedDate = ConfigManager.Config.ServerOption.ServerTime.FixedDate?.Trim();
        if (string.IsNullOrWhiteSpace(fixedDate) ||
            fixedDate.Equals("today", StringComparison.OrdinalIgnoreCase))
            return DateTime.Today;

        if (DateTime.TryParseExact(
                fixedDate,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out var parsed))
            return parsed.Date;

        if (DateTime.TryParse(fixedDate, out parsed))
            return parsed.Date;

        return DateTime.Today;
    }
}
