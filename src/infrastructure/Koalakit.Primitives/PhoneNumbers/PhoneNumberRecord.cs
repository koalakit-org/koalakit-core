namespace Kamel.Primitives.PhoneNumbers;

public sealed record PhoneNumberRecord
{
    public PhoneNumberRecord(string internationalFormat, string nationalFormat, string countryCode, string rFC3966Format, string e164Format)
    {
        InternationalFormat = internationalFormat;
        NationalFormat = nationalFormat;
        CountryCode = countryCode;
        RFC3966Format = rFC3966Format;
        E164Format = e164Format;
    }

    public string InternationalFormat { get; private set; }
    public string NationalFormat { get; private set; }
    public string CountryCode { get; private set; }
    public string RFC3966Format { get; private set; }
    public string E164Format { get; private set; }
}