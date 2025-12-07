namespace Kamel.Primitives;

public enum LanguageCode : byte
{
    Default = 0,
    Arabic,
    English
}

public static class LanguageCodeExtensions
{
    public static string ToCulture(this LanguageCode code) => code switch
    {
        LanguageCode.Default => AcceptLanguages.Arabic,
        LanguageCode.Arabic => AcceptLanguages.Arabic,
        LanguageCode.English => AcceptLanguages.English,
        _ => throw new NotSupportedException()
    };

    public static string ToCode(this LanguageCode code) => code switch
    {
        LanguageCode.Default => "ar",
        LanguageCode.Arabic => "ar",
        LanguageCode.English => "en",
        _ => throw new NotSupportedException()
    };

    public static LanguageCode FromCulture(string? culture) => culture switch
    {
        null => LanguageCode.Arabic,
        AcceptLanguages.Arabic => LanguageCode.Arabic,
        AcceptLanguages.English => LanguageCode.English,
        _ => throw new NotSupportedException($"Unsupported culture code: {culture}")
    };
}