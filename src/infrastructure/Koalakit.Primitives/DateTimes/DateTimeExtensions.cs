namespace Koalakit.Primitives.DateTimes;

public static class DateTimeExtensions
{
    const string ViewDateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
    const string TimeSpanFormat = "yyyyMMdd_HHmmss";
    public static string ToViewDateTimeString(this DateTime? dateTime)
        => dateTime?.ToString("yyyy-MM-dd at HH:mm:ss") ?? string.Empty;

    public static string ToViewDateTimeString(this DateTime dateTime)
        => dateTime.ToString("yyyy-MM-dd at HH:mm:ss");

    public static string ToAbsoluteString(this DateTime dateTime) => dateTime.ToString();
    public static string ToTimestamp(this DateTime dateTime) => dateTime.ToString(TimeSpanFormat);


    public static DateTime ToLocalTime(this DateTime utcDateTime, int offsetMinutes)
    {
        var localTime = utcDateTime.AddMinutes(offsetMinutes);
        return DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
    }

    public static string ToLocalTimeString(this DateTime utcDateTime, int offsetMinutes)
    {
        var localTime = utcDateTime.ToLocalTime(offsetMinutes);
        return localTime.ToString(ViewDateTimeFormat);
    }

    /// <summary>
    /// Calculates age group based on date of birth
    /// </summary>
    /// <param name="dateOfBirth">Date of birth</param>
    /// <param name="currentDate">Current date for comparison</param>
    /// <returns>Age group string</returns>
    public static string GetAgeGroup(this DateOnly? dateOfBirth, DateOnly currentDate)
    {
        if (dateOfBirth == null)
            return "Unknown";

        var age = currentDate.Year - dateOfBirth.Value.Year;
        if (dateOfBirth.Value > currentDate.AddYears(-age)) age--;

        return age switch
        {
            < 1 => "0-12 months",
            1 => "1-2 years",
            >= 2 and < 5 => "2-5 years",
            >= 5 and < 12 => "5-12 years",
            >= 12 and < 18 => "12-18 years",
            _ => "18+ years"
        };
    }
}