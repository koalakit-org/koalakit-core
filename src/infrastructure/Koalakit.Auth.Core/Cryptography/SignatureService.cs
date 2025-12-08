using System.Security.Cryptography;
using System.Text;

namespace Koalakit.Auth.Core.Cryptography;

/// <summary>
/// Service for generating and validating HMAC-SHA256 signatures.
/// </summary>
public class SignatureService : ISignatureService
{
    private readonly byte[] _secretKey;

    /// <summary>
    /// Initializes a new instance of the SignatureService with a byte array secret key.
    /// </summary>
    /// <param name="secretKey">The secret key for HMAC signing</param>
    /// <exception cref="ArgumentException">Thrown when secret key is null or empty</exception>
    public SignatureService(byte[] secretKey)
    {
        if (secretKey == null || secretKey.Length == 0)
        {
            throw new ArgumentException("Secret key cannot be null or empty", nameof(secretKey));
        }
        
        _secretKey = secretKey;
    }

    /// <summary>
    /// Initializes a new instance of the SignatureService with a string secret key.
    /// </summary>
    /// <param name="secretKey">The secret key for HMAC signing (will be UTF-8 encoded)</param>
    /// <exception cref="ArgumentException">Thrown when secret key is null or empty</exception>
    public SignatureService(string secretKey)
        : this(Encoding.UTF8.GetBytes(secretKey ?? throw new ArgumentNullException(nameof(secretKey))))
    {
    }

    /// <inheritdoc/>
    public string GenerateSignature(string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        using var hmac = new HMACSHA256(_secretKey);
        var hash = hmac.ComputeHash(dataBytes);
        return Convert.ToBase64String(hash);
    }

    /// <inheritdoc/>
    public bool ValidateSignature(string data, string signature)
    {
        var expectedSignature = GenerateSignature(data);
        var expectedBytes = Convert.FromBase64String(expectedSignature);
        var providedBytes = Convert.FromBase64String(signature);
        return CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
    }
}

