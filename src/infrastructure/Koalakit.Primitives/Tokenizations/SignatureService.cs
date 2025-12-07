using System.Security.Cryptography;
using System.Text;

namespace Kamel.Primitives.Tokenizations;

public static class SignatureService
{
    private static readonly byte[] SecretKey = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

    public static string GenerateSignature(string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        using var hmac = new HMACSHA256(SecretKey);
        var hash = hmac.ComputeHash(dataBytes);
        return Convert.ToBase64String(hash);
    }

    public static bool ValidateSignature(string data, string signature)
    {
        var expectedSignature = GenerateSignature(data);
        var expectedBytes = Convert.FromBase64String(expectedSignature);
        var providedBytes = Convert.FromBase64String(signature);
        return CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
    }
}