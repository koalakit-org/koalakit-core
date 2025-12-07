namespace Kamel.Primitives.Extensions;

public static class EnumBitFlagsExtensions
{
    public static long ToBitFlags<TEnum>(this IEnumerable<TEnum> values)
        where TEnum : Enum
    {
        long flags = 0;
        foreach (var value in values)
        {
            var bitPosition = Convert.ToInt32(value);
            flags |= 1L << bitPosition;
        }
        return flags;
    }

    public static List<TEnum> FromBitFlags<TEnum>(long bitFlags)
        where TEnum : Enum
    {
        var values = new List<TEnum>();
        for (int bitPosition = 0; bitPosition < 64; bitPosition++)
        {
            if ((bitFlags & 1L << bitPosition) != 0)
            {
                var enumValue = Enum.ToObject(typeof(TEnum), bitPosition);
                if (Enum.IsDefined(typeof(TEnum), enumValue))
                {
                    values.Add((TEnum)enumValue);
                }
            }
        }
        return values;
    }

    public static bool HasBitFlag<TEnum>(long bitFlags, TEnum value)
        where TEnum : Enum
    {
        var bitPosition = Convert.ToInt32(value);
        return (bitFlags & 1L << bitPosition) != 0;
    }
}