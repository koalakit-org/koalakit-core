namespace Kamel.Primitives.DateTimes;

public readonly struct AppDateTime
{
    public static DateTime Unspecified(DateTime? time = null)
    {
        if (time == null)
        {
            return DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        }
        return DateTime.SpecifyKind(time.Value, DateTimeKind.Unspecified);
    }
    public static DateTime NowUtc => DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
    public static DateOnly NowUTCDate => DateOnly.FromDateTime(DateTime.UtcNow);
    public static DateOnly MinDateValue => DateOnly.MinValue;
    public static long NotUtcTicks => DateTime.SpecifyKind(NowUtc, DateTimeKind.Unspecified).Ticks;
    public static string NowTimestamp => NowUtc.ToString("yyyyMMdd_HHmmss");
    public static DateTime FromTicks(long ticks)
    {
        return new DateTime(ticks);
    }

    public static DateTime FromTicks(string ticksString)
    {
        long ticks = long.Parse(ticksString);
        return new DateTime(ticks);
    }

    public static DateTime NowLocal(int offsetMinutes) => NowUtc.ToLocalTime(offsetMinutes);
}