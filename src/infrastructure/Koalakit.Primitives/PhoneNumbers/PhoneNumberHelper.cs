using PhoneNumbers;

namespace Kamel.Primitives.PhoneNumbers;

public static class PhoneNumberHelper
{
    private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

    public static bool Validate(string? number, string region = "SA")
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        region = region.ToUpperInvariant();
        if (!phoneNumberUtil.GetSupportedRegions().Contains(region))
            return false;

        try
        {
            var numberProto = phoneNumberUtil.Parse(number, region);
            return phoneNumberUtil.IsValidNumberForRegion(numberProto, region);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    public static string GetInternationalFormat(string? number, string region = "SA")
    {
        if (TryDetails(number, out var details, region) == false || details is null)
        {
            throw new ArgumentException("Invalid phone number format.", nameof(number));
        }
        return details.InternationalFormat;
    }


    public static bool TryDetails(string? number, out PhoneNumberRecord? result, string region = "SA")
    {
        result = default;
        try
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            var numberProto = phoneNumberUtil.Parse(number, region.ToUpper());
            if (phoneNumberUtil.IsValidNumber(numberProto))
            {
                result = new PhoneNumberRecord(
                    phoneNumberUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL),
                    phoneNumberUtil.Format(numberProto, PhoneNumberFormat.NATIONAL),
                    numberProto.CountryCode.ToString(),
                    phoneNumberUtil.Format(numberProto, PhoneNumberFormat.RFC3966),
                    phoneNumberUtil.Format(numberProto, PhoneNumberFormat.E164));
                return true;
            }
            return false;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}