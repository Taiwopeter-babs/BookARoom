using System.Globalization;

namespace BookARoom.Extensions;

public static class ConvertDateTimeToUtc
{
    /// <summary>
    /// converts a date string to a utc datetime format
    /// </summary>
    /// <param name="dateString">datetime string to convert</param>
    /// <returns>A UTC Datetime object</returns>
    public static DateTime GetUtcDate(this string dateString)
    {
        var provider = CultureInfo.InvariantCulture;
        DateTime datetimeUtc;

        datetimeUtc = DateTime.ParseExact(dateString, "dd/MM/yyyy", provider);

        datetimeUtc = DateTime.SpecifyKind(datetimeUtc, DateTimeKind.Utc);

        return datetimeUtc;
    }
}
