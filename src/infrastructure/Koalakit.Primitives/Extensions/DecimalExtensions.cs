namespace Kamel.Primitives.Extensions;

public static class DecimalExtensions
{
    public static decimal Round(this decimal value, int decimals = 2)
    {
        return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
    }
}